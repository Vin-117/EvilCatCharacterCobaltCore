using EvilCat.Cards;
using EvilCat.External;
using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace EvilCat.Actions;



///
/// Function which prompts user to pick a card from exhaust pile to send to draw pile. This is not optional.
/// 
public class ARestorePickCardToDraw : CardAction
{
    public required int count; //User must pass number of cards that must be returned to draw pile
    public bool ToDiscard = false;
    private CardDestination source = CardDestination.Exhaust; //Define source to select from (hand)
    private CardDestination destination = CardDestination.Deck; //Define where to send cards (draw pile)

    public override Route? BeginWithRoute(G g, State s, Combat c)
    {

        if (ToDiscard) 
        {
            destination = CardDestination.Discard;
        }

        //Setup card browse action
        CardBrowse cardBrowse = new CardBrowse
        {
            mode = CardBrowse.Mode.Browse,
            browseSource = CardBrowse.Source.ExhaustPile,
            browseAction = new AMultiBrowseRestoreActions { count = count, ToDiscard = ToDiscard },
            allowCancel = false,
            ignoreCardType = new EvilCatRestore().Key()
        };

        //Do nothing if there is nothing in source pile
        if (cardBrowse.GetCardList(g).Count == 0)
        {
            return null;
        }

        //Define browse route
        var multiBrowseRoute = ModEntry.Instance.KokoroApi.MultiCardBrowse.MakeRoute(cardBrowse);

        //Player MUST return number of cards equal to count, where possible
        multiBrowseRoute.MaxSelected = count;
        multiBrowseRoute.MinSelected = count;

        timer = 0.0; //Make this happen instantly

        return multiBrowseRoute.AsRoute;
    }

}



///
/// Function which queues restore's actions for multibrowse
/// 
public class AMultiBrowseRestoreActions : CardAction
{

    public int count;
    public bool ToDiscard;

    public override void Begin(G g, State s, Combat c)
    {
        //Check to make sure we aren't doing anything to cards that aren't selected
        if (ModEntry.Instance.KokoroApi.MultiCardBrowse.GetSelectedCards(this) is not { } selectedCards)
        {
            return;
        }

        //Return all selected cards to draw pile.
        foreach (var card in selectedCards)
        {
            var action = new ARestoreTargetCard { ToDiscard = ToDiscard };
            action.selectedCard = card;
            c.QueueImmediate(action);
        }
    }

    public override string? GetCardSelectText(State s)
    {

        if (ToDiscard)
        {
            if (count == 1)
            {
                return "Pick an <c=cardtrait>exhausted</c> card. It is placed in your discard pile.\n(You cannot pick Restore!)";
            }
            else
            {
                return $"Pick {count} <c=cardtrait>exhausted</c> cards. They are placed in your discard pile.\n(You cannot pick Restore!)";
            }
        }
        else 
        {
            if (count == 1)
            {
                return "Pick an <c=cardtrait>exhausted</c> card. It is shuffled into your draw pile.\n(You cannot pick Restore!)";
            }
            else
            {
                return $"Pick {count} <c=cardtrait>exhausted</c> cards. They are shuffled into your draw pile.\n(You cannot pick Restore!)";
            }
        }
    }
}




///
/// Function which actually moves the targetted cards to draw pile.
/// 
public class ARestoreTargetCard : CardAction
{

    public bool ToDiscard = false;

    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0;

        //If no card selected
        if (selectedCard is null)
        {
            return; //Do nothing
        }

        Card? card = s.FindCard(selectedCard.uuid); //Get card ID

        //If card is not null and exhaust pile contains this card
        if (card != null && c.exhausted.Contains(card))
        {
            s.RemoveCardFromWhereverItIs(card.uuid); //Remove it from exhaust

            if (ToDiscard) //Send to discard if that's what we want
            {
                s.RemoveCardFromWhereverItIs(card.uuid);
                card.OnDiscard(s, c);
                c.SendCardToDiscard(s, card);
            }
            else //Else shuffle it back into the deck at random
            {
                s.SendCardToDeck(card, doAnimation: true, insertRandomly: true);
            }
        }
    }

}
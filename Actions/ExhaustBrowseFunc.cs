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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EvilCat.Actions;



///
/// Function which exhausts a selected card
/// 
public class AExhaustTargetCard : CardAction
{

    public bool fromDraw = false;

    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0; //make the exhaust play out instantly

        //If no card selected
        if (selectedCard is null)
        {
            return; //Do nothing
        }
        
        Card? card = s.FindCard(selectedCard.uuid); //Get card ID

        if (fromDraw)
        {
            //If card is not null and the draw pile contains the card
            if (card != null && s.deck.Contains(card))
            {
                Audio.Play(Event.CardHandling);
                card.ExhaustFX();
                s.deck.Remove(card); //Remove card from hand
                c.SendCardToExhaust(s, card); //Send to exhaust pile
            }
        }
        else 
        {
            //If card is not null and hand contains the card
            if (card != null && c.hand.Contains(card))
            {
                Audio.Play(Event.CardHandling);
                card.ExhaustFX();
                c.hand.Remove(card); //Remove card from hand
                c.SendCardToExhaust(s, card); //Send to exhaust pile
            }
        }
    }
}



///
/// Function which queues exhaust actions for multibrowse
/// 
public class AMultiBrowseExhaustActions : CardAction
{

    public int count = 0;
    public bool optional = false;
    public bool fromDraw = false;

    public override void Begin(G g, State s, Combat c)
    {
        //Check to make sure we aren't doing anything to cards that aren't selected
        if (ModEntry.Instance.KokoroApi.MultiCardBrowse.GetSelectedCards(this) is not { } selectedCards)
        {
            return;
        }

        //Exhaust all selected cards
        foreach (var card in selectedCards)
        {
            var action = new AExhaustTargetCard { fromDraw = fromDraw };
            action.selectedCard = card;
            c.QueueImmediate(action);
        }
    }

    public override string? GetCardSelectText(State s)
    {

        if (fromDraw) 
        {
            if (count == 1)
            {
                return "Pick a card in your draw pile to <c=cardtrait>exhaust</c>.";
            }
            else
            {
                return $"Pick {count} cards in your draw pile to <c=cardtrait>exhaust</c>.";
            }
        }
        if (optional)
        {
            if (count == 1)
            {
                return "Pick a card in your hand to <c=cardtrait>exhaust</c>.\n(This is optional.)";
            }
            else
            {
                return $"Pick up to {count} cards in your hand to <c=cardtrait>exhaust</c>.\n(This is optional.)";
            }
        }
        else 
        {
            if (count == 1)
            {
                return "Pick a card in your hand to <c=cardtrait>exhaust</c>.";
            }
            else
            {
                return $"Pick {count} cards in your hand to <c=cardtrait>exhaust</c>.";
            }
        }

    }


}
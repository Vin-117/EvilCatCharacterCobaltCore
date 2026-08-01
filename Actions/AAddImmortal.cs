using EvilCat.Actions;
using EvilCat.External;
using EvilCat.Features;
using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace EvilCat.Actions;






public class ADangerousUnlockSelect : CardAction
{
    private int count = 2; //Fixed at two since this method is only meant to be used for a specific artifact

    public override Route? BeginWithRoute(G g, State s, Combat c)
    {
        //Setup card browse action
        CardBrowse cardBrowse = new CardBrowse
        {
            mode = CardBrowse.Mode.Browse,
            browseSource = CardBrowse.Source.Deck,
            browseAction = new AMultiBrowseDangerousUnlockSelect { count = count },
            allowCancel = false
        };

        //Do nothing if there is nothing in source pile
        if (cardBrowse.GetCardList(g).Count == 0)
        {
            return null;
        }

        //Define browse route
        var multiBrowseRoute = ModEntry.Instance.KokoroApi.MultiCardBrowse.MakeRoute(cardBrowse);

        //Player MUST exhaust number of cards equal to count, where possible
        multiBrowseRoute.MaxSelected = count;
        multiBrowseRoute.MinSelected = count;

        timer = 0.0; //Make this happen instantly

        return multiBrowseRoute.AsRoute;
    }
}


public class AMultiBrowseDangerousUnlockSelect : CardAction
{

    public int count = 0;

    public override void Begin(G g, State s, Combat c)
    {
        //Check to make sure we aren't doing anything to cards that aren't selected
        if (ModEntry.Instance.KokoroApi.MultiCardBrowse.GetSelectedCards(this) is not { } selectedCards)
        {
            return;
        }

        //Add immortal to each selected card
        foreach (var card in selectedCards)
        {
            var action = new AAddImmortal { isPermanent = true };
            action.selectedCard = card;
            s.GetCurrentQueue().QueueImmediate(action);
        }
    }

    public override string? GetCardSelectText(State s)
    {
        return $"Pick {count} cards in your deck. They gain <c=cardtrait>persistent</c>.";
    }
}



///
/// Function which adds immortal trait to a selected card.
///
public class AAddImmortal : CardAction
{

    public required bool isPermanent;

    public override void Begin(G g, State s, Combat c)
    {

        Card card = selectedCard!; //Define variable to store selected card


        if (isPermanent)
        {
            //If the card is NOT null
            if (card != null)
            {
                //If the card does not already have the immortal trait
                if (!ImmortalTraitExt.GetIsImmortal(card))
                {
                    //And if the card does not already have the temp immortal trait
                    if (!TEMPImmortalTraitExt.GetIsTEMPImmortal(card))
                    {
                        ModEntry.Instance.helper.Content.Cards.SetCardTraitOverride(s, card, ModEntry.Instance.EvilCatImmortalTrait, true, true);
                        ImmortalTraitExt.SetIsImmortal(card, true);
                    }
                }
            }
        }
        else 
        {
            //If the card is NOT null
            if (card != null)
            {
                //If the card does not already have the immortal trait
                if (!ImmortalTraitExt.GetIsImmortal(card))
                {

                    //And if the card does not already have the temp immortal trait
                    if (!TEMPImmortalTraitExt.GetIsTEMPImmortal(card))
                    {
                        ModEntry.Instance.helper.Content.Cards.SetCardTraitOverride(s, card, ModEntry.Instance.TEMPEvilCatImmortalTrait, true, false);
                        TEMPImmortalTraitExt.SetIsTEMPImmortal(card, true);
                    }
                }
            }

        }

    }

}



public class AAddImmortalAlt : CardAction
{

    private bool alreadyPersistent = false;

    public override Route? BeginWithRoute(G g, State s, Combat c)
    {
        Card card = selectedCard!;
        if (card != null)
        {

            alreadyPersistent = ImmortalTraitExt.GetIsImmortal(card);


            //Add persistent if the card does not already have it.
            if (!alreadyPersistent)
            {
                ModEntry.Instance.helper.Content.Cards.SetCardTraitOverride(s, card, ModEntry.Instance.EvilCatImmortalTrait, true, true);
                ImmortalTraitExt.SetIsImmortal(card, true);
            }

            if (!alreadyPersistent)
            {
                return new ShowCardsStrFix
                {
                    messageKey = "Added <c=cardtrait>persistant</c>!",
                    cardIds = new List<int> { card.uuid }
                };
            }
        }
        return null;
    }

    public override string? GetCardSelectText(State s)
    {
        return "Select a card to gain <c=cardtrait>persistant</c>, forever";
    }
}
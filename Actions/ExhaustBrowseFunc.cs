using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EvilCat.External;

namespace EvilCat.Actions;



///
/// Function which exhausts a selected card
/// 
public class AExhaustTargetCard : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0; //make the exhaust play out instantly

        //If no card selected
        if (selectedCard is null)
        {
            return; //Do nothing
        }
        
        Card? card = s.FindCard(selectedCard.uuid); //Get card ID

        //If card is not null and hand contains the card
        if (card != null && c.hand.Contains(card))
        {
            Audio.Play(Event.CardHandling);
            c.hand.Remove(card); //Remove card from hand
            //c.RenderExhaust(g);
            c.SendCardToExhaust(s, card); //Send to exhaust pile
        }
    }
}



///
/// Function which queues actions for multibrowse
/// 
public class AMultiBrowseExhaustActions : CardAction
{
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
            var action = new AExhaustTargetCard { };
            action.selectedCard = card;
            c.QueueImmediate(action);
        }
    }
}
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




public class AFullKernelAccessToDiscard : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        Card card = selectedCard!;
        if (card != null)
        {
            s.RemoveCardFromWhereverItIs(card.uuid);
            c.SendCardToHand(s, card);
            c.TryPlayCard(s, card, playNoMatterWhatForFree: true);
            s.RemoveCardFromWhereverItIs(card.uuid);
            c.SendCardToDiscard(s, card);
        }
    }
}




public class AFullKernelAccessToTopDeck : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        Card card = selectedCard!;
        if (card != null)
        {
            s.RemoveCardFromWhereverItIs(card.uuid);
            c.SendCardToHand(s, card);
            c.TryPlayCard(s, card, playNoMatterWhatForFree: true);
            s.RemoveCardFromWhereverItIs(card.uuid);
            s.SendCardToDeck(card, doAnimation: true, insertRandomly: false);
        }
    }
}
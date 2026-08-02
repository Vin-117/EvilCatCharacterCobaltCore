using EvilCat.Actions;
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

namespace EvilCat.Actions;



/// 
/// Function which adds a number of SegFaults to hand
/// 
public class AAddSegFault : CardAction
{
    private Card card = new EvilCatSegFault();
    public static Spr AAddSegFaultSpr; 
    private CardDestination destination = CardDestination.Hand;
    public required int amount;
    private bool showCardTraitTooltips = false;
    public int? handPosition;

    //public bool callItTheDeckNotTheDrawPile;

    public override void Begin(G g, State s, Combat c)
    {

        timer = 0.3;
        if (s.route is Combat)
        {
            card.pos = new Vec(G.screenSize.x * 0.5 - 30.0, 30.0);
            card.waitBeforeMoving = timer;
            card.drawAnim = 1.0;
            foreach (Artifact item in g.state.EnumerateAllArtifacts())
            {
                item.OnPlayerRecieveCardMidCombat(g.state, c, card);
            }
        }

        c?.SendCardToHand(s, card, handPosition);

        Audio.Play(Event.CardHandling);
        s.DebugSafeIdCheck();
        if (amount > 1)
        {
            amount--;
            s.GetCurrentQueue().QueueImmediate(new AAddSegFault
            {
                amount = amount
            });
        }
    }

    public override List<Tooltip> GetTooltips(State s)
    {

        if (amount == 1)
        {
            return new List<Tooltip>
            {
                new GlossaryTooltip(key: "AAddSegFault")
                {
                    Icon = AAddSegFaultSpr,
                    TitleColor = Colors.card,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "AAddSegFault", "title"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "AAddSegFault", "desc_single"])
                },
                new TTCard
                {
                    card = new EvilCatSegFault()
                    {
                        upgrade = Upgrade.None,
                        temporaryOverride = true
                    }
                },
            };
        }
        else 
        {
            return new List<Tooltip>
            {
                new GlossaryTooltip(key: "AAddSegFault")
                {
                    Icon = AAddSegFaultSpr,
                    TitleColor = Colors.card,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "AAddSegFault", "title"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "AAddSegFault", "desc"], new { cnt = amount })
                },
                new TTCard
                {
                    card = new EvilCatSegFault()
                    {
                        upgrade = Upgrade.None,
                        temporaryOverride = true
                    }
                },
            };
        }
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon
        {
            path = AAddSegFaultSpr,
            number = amount,
            color = Colors.textMain
        };
    }
}
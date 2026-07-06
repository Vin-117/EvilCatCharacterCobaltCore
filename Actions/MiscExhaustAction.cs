using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EvilCat.Cards;
using EvilCat.External;
using static EvilCat.External.IKokoroApi.IV2;
using static EvilCat.External.IKokoroApi.IV2.IStatusLogicApi;
using static EvilCat.External.IKokoroApi.IV2.IStatusLogicApi.IHook;

namespace EvilCat.Actions;

public sealed class EvilCatxhaustCardBrowseAction : CardAction
{
    public required List<CardAction>? OnSuccess;
    public int CardCost;
    public override void Begin(G g, State s, Combat c)
    {
        base.Begin(g, s, c);
        if (selectedCard is null)
            return;

        CardCost = selectedCard.GetCurrentCost(s);

        c.QueueImmediate([
            new EvilCatExhaustOtherCard { uuid = selectedCard.uuid },
                ..(OnSuccess ?? [])
        ]);
    }
}

public sealed class EvilCatExhaustOtherCard : AExhaustOtherCard
{
    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0;
        if (s.FindCard(uuid) is not { } card)
            return;

        card.ExhaustFX();
        Audio.Play(Event.CardHandling);
        s.RemoveCardFromWhereverItIs(uuid);
        c.SendCardToExhaust(s, card);
        timer = 0.3;
    }
}







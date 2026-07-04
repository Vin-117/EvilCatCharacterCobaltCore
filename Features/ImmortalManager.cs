using EvilCat.Actions;
using EvilCat.Cards;
using EvilCat.External;
using HarmonyLib;
using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading;
using static EvilCat.External.IKokoroApi.IV2;
using static EvilCat.External.IKokoroApi.IV2.IStatusLogicApi;
using static EvilCat.External.IKokoroApi.IV2.IStatusLogicApi.IHook;

namespace EvilCat.Features;

internal static class ImmortalTraitExt
{
    public static bool GetIsImmortal(this Card self)
        => ModEntry.Instance.Helper.ModData.GetModDataOrDefault<bool>(self, "ImmortalTrait");

    public static void SetIsImmortal(this Card self, bool value)
        => ModEntry.Instance.Helper.ModData.SetModData(self, "ImmortalTrait", value);
}


internal sealed class ImmortalTraitManager
{
    internal static readonly ICardTraitEntry Trait = ModEntry.Instance.EvilCatImmortalTrait;

    public ImmortalTraitManager()
    {
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AEndTurn), nameof(AEndTurn.Begin)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AEndTurn_Begin_Prefix))
        );

        //s.deck.Remove(card);
        //s.RemoveCardFromWhereverItIs(card.uuid);
        //c.SendCardToDiscard(s, card);
    }

    private static void AEndTurn_Begin_Prefix(State s, Combat c)
    {

        if (c.cardActions.Any(a => a is AEndTurn))
            return;

        //List<int> tar_indices = Enumerable.Repeat(0, n);

        foreach (Card card in c.exhausted.ToList())
        {
            if (card.GetIsImmortal())
            {
                c.exhausted.Remove(card);
                c.SendCardToDiscard(s, card);
            }
        }

    }
}
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

internal static class TEMPImmortalTraitExt
{
    public static bool GetIsTEMPImmortal(this Card self)
        => ModEntry.Instance.Helper.ModData.GetModDataOrDefault<bool>(self, "TEMPImmortalTrait");

    public static void SetIsTEMPImmortal(this Card self, bool value)
        => ModEntry.Instance.Helper.ModData.SetModData(self, "TEMPImmortalTrait", value);

    public static void RemoveTEMPImmortal(this Card self, State s)
    {
        SetIsTEMPImmortal(self, false);
        ModEntry.Instance.helper.Content.Cards.SetCardTraitOverride(s, self, ModEntry.Instance.TEMPEvilCatImmortalTrait, false, false);
    }

}


internal sealed class TEMPImmortalTraitManager
{
    internal static readonly ICardTraitEntry Trait = ModEntry.Instance.TEMPEvilCatImmortalTrait;

    public TEMPImmortalTraitManager()
    {
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AEndTurn), nameof(AEndTurn.Begin)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AEndTurn_Begin_Prefix))
        );

        ModEntry.Instance.Helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnCombatEnd), (State state) =>
        {
            foreach (var card in state.deck)
            {
                if (card.GetIsTEMPImmortal())
                {
                    card.RemoveTEMPImmortal(state);
                }
            }
        });
    }

    private static void AEndTurn_Begin_Prefix(State s, Combat c)
    {

        if (c.cardActions.Any(a => a is AEndTurn))
            return;

        //List<int> tar_indices = Enumerable.Repeat(0, n);

        foreach (Card card in c.exhausted.ToList())
        {
            if (card.GetIsTEMPImmortal())
            {
                c.exhausted.Remove(card);
                c.SendCardToDiscard(s, card);
            }
        }

    }

}
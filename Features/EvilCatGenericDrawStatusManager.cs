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



///
/// Status which makes the player draw extra cards every turn
/// 
public class EvilCatGenericDrawStatusManager : IKokoroApi.IV2.IStatusLogicApi.IHook, IKokoroApi.IV2.IStatusRenderingApi.IHook
{

    /// 
    /// Register hooks
    /// 
    public EvilCatGenericDrawStatusManager() 
    {
        ModEntry.Instance.KokoroApi.StatusLogic.RegisterHook(this, 0);
        ModEntry.Instance.KokoroApi.StatusRendering.RegisterHook(this, 0);
    }


    ///
    /// Define status behaviour
    /// 
    public bool HandleStatusTurnAutoStep(IHandleStatusTurnAutoStepArgs args)
    {


        ///
        /// Do nothing if either the hook does not detect the status, 
        /// or its not the start of the turn.
        ///
        if (args.Status != ModEntry.Instance.EvilCatGenericDrawStatus.Status)
        {
            return false;
        }
        if (args.Timing != StatusTurnTriggerTiming.TurnStart)
        {
            return false;
        }
        ///
        /// Will only get here if its the start of the turn and is the correct status,
        /// so then draw equal to status
        ///
        if (args.Amount > 0)
        {
            args.Combat.QueueImmediate
            (
                new ADrawCard()
                {
                    count = args.Amount,
                    timer = 1
                }
            );
        }
        return false;
    }
}
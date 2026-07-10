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



public class EvilCatTempShieldExhaustStatusManager : IKokoroApi.IV2.IStatusLogicApi.IHook, IKokoroApi.IV2.IStatusRenderingApi.IHook
{

    /// 
    /// Register hooks
    /// 
    public EvilCatTempShieldExhaustStatusManager() 
    {
        ModEntry.Instance.KokoroApi.StatusLogic.RegisterHook(this, 0);
        ModEntry.Instance.KokoroApi.StatusRendering.RegisterHook(this, 0);
    }

}
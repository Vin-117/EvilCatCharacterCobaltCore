using EvilCat.Features;
using FMOD;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EvilCat.Artifacts;

public class EvilCatThreadMismatch : Artifact, IRegisterable
{

    private static Spr UsedUpSpr;

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        UsedUpSpr = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/ThreadMismatch_off.png")).Sprite;

        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = ModEntry.Instance.EvilCatDeck.Deck,
                unremovable = false
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatThreadMismatch", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatThreadMismatch", "desc"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/ThreadMismatch.png")).Sprite,

        });



        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.SendCardToExhaust)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(EvilCatThreadMismatchPostfix))
        );
    }

    public bool EvilCatThreadMismatchUsed = false;

    public override void OnCombatEnd(State state)
    {
        EvilCatThreadMismatchUsed = false;
    }

    public override Spr GetSprite()
    {
        if (EvilCatThreadMismatchUsed)
        {
            return UsedUpSpr;
        }
        else
        {
            return base.GetSprite();
        }
    }

    private static void EvilCatThreadMismatchPostfix(State s, Card card, ref Combat __instance)
    {

        if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is EvilCatThreadMismatch) is not { } artifact)
            return;

        var EvilCatThreadMismatchVar = (EvilCatThreadMismatch)artifact;

        if (EvilCatThreadMismatchVar.EvilCatThreadMismatchUsed)
            return;

        //This code should only run once, if a card has been exhausted, the player has this
        //artifact, and the effect hasn't been triggered before this combat
        __instance.Queue(new ADrawCard { count = 3 });
        artifact.Pulse();
        EvilCatThreadMismatchVar.EvilCatThreadMismatchUsed = true;

    }



}
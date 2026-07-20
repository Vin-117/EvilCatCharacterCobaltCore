using EvilCat.Actions;
using EvilCat.Cards;
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

public class EvilCatFullKernel : Artifact, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Boss],
                owner = ModEntry.Instance.EvilCatDeck.Deck,
                unremovable = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatFullKernel", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatFullKernel", "desc"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/FullKernel.png")).Sprite
        });

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.SendCardToExhaust)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(EvilCatFullKernelPostfix))
        );



    }

    public int KernelExhaustCount = 0;

    public override int? GetDisplayNumber(State s)
    {
        return KernelExhaustCount;
    }

    private static void EvilCatFullKernelPostfix(State s, Card card, ref Combat __instance)
    {
        if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is EvilCatFullKernel) is not { } artifact)
            return;

        var kernelartifact = (EvilCatFullKernel)artifact;

        if (kernelartifact.KernelExhaustCount == 1)
        {
            __instance.Queue
                (new AEnergy
                {
                    changeAmount = 1
                });
            kernelartifact.KernelExhaustCount = 0;
            artifact.Pulse();
        }
        else
        {
            kernelartifact.KernelExhaustCount++;
        }
    }




}
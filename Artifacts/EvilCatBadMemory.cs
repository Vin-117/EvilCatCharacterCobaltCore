using EvilCat.Actions;
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

public class EvilCatBadMemory : Artifact, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatBadMemory", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatBadMemory", "desc"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/SaveCorruption.png")).Sprite,

        });


        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.SendCardToExhaust)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(EvilCatBadMemoryPostfix))
        );

    }

    public override void OnReceiveArtifact(State state)
    {
        state.ship.baseEnergy++;
    }

    public override void OnRemoveArtifact(State state)
    {
        state.ship.baseEnergy--;
    }

    private static void EvilCatBadMemoryPostfix(State s, Card card, ref Combat __instance)
    {


        if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is EvilCatBadMemory) is not { } artifact)
            return;

        //var exhausted_card_deck = card.GetMeta().deck;


        __instance.Queue(new AAddRandomCard { destination = CardDestination.Hand });
        artifact.Pulse();

    }


}
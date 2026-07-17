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

    }

    public override void OnReceiveArtifact(State state)
    {
        state.ship.baseEnergy++;
    }

    public override void OnRemoveArtifact(State state)
    {
        state.ship.baseEnergy--;
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return new List<Tooltip>
        {
            new TTCard
            {
                card = new EvilCatSegFault()
                {
                    upgrade = Upgrade.A
                }
            }
        };
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        combat.QueueImmediate(new AAddCard
        {
            card = new EvilCatSegFault()
            {
                upgrade = Upgrade.A
            },
            artifactPulse = Key(),
            destination = CardDestination.Hand,
            amount = 4
        });
    }


}
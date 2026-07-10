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
using EvilCat.Features;

namespace EvilCat.Artifacts;

public class EvilCatDangerousUnlock : Artifact, IRegisterable
{

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = ModEntry.Instance.EvilCatDeck.Deck,
                unremovable = false
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatDangerousUnlock", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatDangerousUnlock", "desc"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/DangerouUnlock.png")).Sprite,

        });
    }


    public override void OnReceiveArtifact(State state)
    {
        state.GetCurrentQueue().QueueImmediate(new ACardSelect
        {
            browseAction = new AAddImmortal { isPermanent = true },
            browseSource = CardBrowse.Source.Deck
        });
        state.GetCurrentQueue().QueueImmediate(new ACardSelect
        {
            browseAction = new AAddImmortal { isPermanent = true },
            browseSource = CardBrowse.Source.Deck
        });
        state.GetCurrentQueue().QueueImmediate(new AAddCard
        {
            amount = 1,
            card = new EvilCatSegFault { temporaryOverride = false },
        });

    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return new List<Tooltip>
        {
            new TTCard
            {
                card = new EvilCatSegFault()
                {
                    upgrade = Upgrade.None,
                    temporaryOverride = false
                }
            }
        };
    }

}
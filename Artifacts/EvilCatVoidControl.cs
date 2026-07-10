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

public class EvilCatVoidControl : Artifact, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatVoidControl", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatVoidControl", "desc"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/VoidControl.png")).Sprite,

        });

    }


    public override void OnCombatEnd(State state)
    {
        state.rewardsQueue.QueueImmediate(new ACardSelect
        {
            browseAction = new CardSelectDuplicate(),
            browseSource = CardBrowse.Source.Deck,
            filterExhaust = true,
            allowCloseOverride = true
        });
    }


}
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

public class EvilCatBackupData : Artifact, IRegisterable
{

    private static Spr UsedUpSpr;

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        UsedUpSpr = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/BackupSave_off.png")).Sprite;

        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = ModEntry.Instance.EvilCatDeck.Deck,
                unremovable = false
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatBackupData", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EvilCatBackupData", "desc"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Artifact/BackupSave.png")).Sprite,

        });



        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.SendCardToExhaust)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(EvilCatBackupDataPostfix))
        );
    }

    public bool EvilCatBackupUsed = false;

    public override void OnCombatEnd(State state)
    {
        EvilCatBackupUsed = false;
    }

    public override Spr GetSprite()
    {
        if (EvilCatBackupUsed)
        {
            return UsedUpSpr;
        }
        else
        {
            return base.GetSprite();
        }
    }

    private static void EvilCatBackupDataPostfix(State s, Card card, ref Combat __instance)
    {

        if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is EvilCatBackupData) is not { } artifact)
            return;

        var EvilCatBackupDataVar = (EvilCatBackupData)artifact;

        if (EvilCatBackupDataVar.EvilCatBackupUsed)
            return;

        //This code should only run once, if a card has been exhausted, the player has this
        //artifact, and the effect hasn't been triggered before this combat
        __instance.Queue(new ADrawCard { count = 2 });
        artifact.Pulse();
        EvilCatBackupDataVar.EvilCatBackupUsed = true;

    }



}
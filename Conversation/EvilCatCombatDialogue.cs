using FMOD;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using static EvilCat.Conversation.CommonDefinitions;
using EvilCat.External;

namespace EvilCat.Conversation;

internal class EvilCatCombatDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {

            //Dialogue for player hitting a shot
            {
                "EvilCat_Dialogue_ShotHitGeneric_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Shot hit.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHitGeneric_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Hit connected.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHitGeneric_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Permanent damage.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHitGeneric_3", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Good.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHitGeneric_4", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Damage confirmed.")
                    ]
                }
            },


            //Dialogue for when Evil Cat hits a shot
            {
                "EvilCat_Dialogue_ShotHit_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "feral", "Got you.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHit_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "Just exploiting an opening.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHit_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Got them!")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ShotHit_3", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_WeHit_Tag"],
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "This isn't so hard!")
                    ]
                }
            },

            //Dialogue for when Evil Cat does big damage
            {
                "EvilCat_Dialogue_BigDMGShotHit_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_BigHit_Tag"],
                    minDamageDealtToEnemyThisAction = 3,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "I'm pretty good, right?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_BigDMGShotHit_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_BigHit_Tag"],
                    minDamageDealtToEnemyThisAction = 3,
                    dialogue =
                    [
                        new(AmEvilCat, "feral", "That was satisfying.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_BigDMGShotHit_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustHit = true,
                    whoDidThatName = AmEvilCat,
                    oncePerCombatTags = ["EvilCat_BigHit_Tag"],
                    minDamageDealtToEnemyThisAction = 3,
                    dialogue =
                    [
                        new(AmEvilCat, "feral", "Are you afraid yet?")
                    ]
                }
            },

            //Dialogue for when the enemy gains autododge
            {
                "EvilCat_Dialogue_AutoDodgeLeft_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    lastTurnEnemyStatuses = [Status.autododgeLeft],
                    oncePerCombatTags = ["EvilCat_AutoDodge_Tag"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "They're going to dodge left.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_AutoDodgeLeft_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    lastTurnEnemyStatuses = [Status.autododgeLeft],
                    oncePerCombatTags = ["EvilCat_AutoDodge_Tag"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "This'll be tricky...")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_AutoDodgeLeft_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    lastTurnEnemyStatuses = [Status.autododgeLeft],
                    oncePerCombatTags = ["EvilCat_AutoDodge_Tag"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "angry", "Don't let them escape!")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_AutoDodgeRight_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    lastTurnEnemyStatuses = [Status.autododgeRight],
                    oncePerCombatTags = ["EvilCat_AutoDodge_Tag"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "They're going to dodge right.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_AutoDodgeRight_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    lastTurnEnemyStatuses = [Status.autododgeRight],
                    oncePerCombatTags = ["EvilCat_AutoDodge_Tag"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "This'll be tricky...")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_AutoDodgeRight_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    lastTurnEnemyStatuses = [Status.autododgeRight],
                    oncePerCombatTags = ["EvilCat_AutoDodge_Tag"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "angry", "Don't let them escape!")
                    ]
                }
            },

            //Dialogue for when the player misses a shot
            {
                "EvilCat_Dialogue_WeMissed_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustMissed = true,
                    oncePerCombatTags = ["EvilCat_WeMissed_Tag"],
                    doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Shot missed.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeMissed_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustMissed = true,
                    oncePerCombatTags = ["EvilCat_WeMissed_Tag"],
                    doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "Why shoot empty space?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeMissed_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustMissed = true,
                    oncePerCombatTags = ["EvilCat_WeMissed_Tag"],
                    doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Off target.")
                    ]
                }
            },

            //Dialogue for when the player misses but has recalibrator
            {
                "EvilCat_Dialogue_WeMissedButHaveRecalibrator_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustMissed = true,
                    oncePerCombatTags = ["EvilCat_WeMissed_Tag"],
                    hasArtifacts = [ "Recalibrator" ],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "Totally calculated.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeMissedButHaveRecalibrator_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    playerShotJustMissed = true,
                    oncePerCombatTags = ["EvilCat_WeMissed_Tag"],
                    hasArtifacts = [ "Recalibrator" ],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Recalibrating!")
                    ]
                }
            },

            //Dialogue related to playing many cards
            {
                "EvilCat_Dialogue_ManyCards_0", new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = ["EvilCatManyCardsPlayed"],
                    oncePerCombat = true,
                    minCardsPlayedThisTurn = 6,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "So much is happening!")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ManyCards_1", new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = ["EvilCatManyCardsPlayed"],
                    oncePerCombat = true,
                    minCardsPlayedThisTurn = 6,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Is everyone keeping up with this?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ManyCards_2", new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = ["EvilCatManyCardsPlayed"],
                    oncePerCombat = true,
                    minCardsPlayedThisTurn = 6,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "We're really good, aren't we?")
                    ]
                }
            },

            //Dialogue for when the hand is full of garbage or unplayable cards
            {
                "EvilCat_Dialogue_HandofUnplayable_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    handFullOfUnplayableCards = true,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "This is useless, isn't it?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_HandofGarbage_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    handFullOfTrash = true,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "...I may have gone overboard with the trash.")
                    ]
                }
            },

            //Dialogue for when the hand is empty
            {
                "EvilCat_Dialogue_HandEmpty_0", new()
                {
                    type = NodeType.combat,
                    handEmpty = true,
                    minEnergy = 1,
                    oncePerCombatTags = [ "EvilCatHandEmpty" ],
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "Guess that's that.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_HandEmpty_1", new()
                {
                    type = NodeType.combat,
                    handEmpty = true,
                    minEnergy = 1,
                    oncePerCombatTags = [ "EvilCatHandEmpty" ],
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Nothing left?")
                    ]
                }
            },


            //Dialogue for when the players ship takes reduced damage due to armor
            {
                "EvilCat_Dialogue_ArmorDeflectedDMG_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    minDamageBlockedByPlayerArmorThisTurn = 1,
                    oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Armor deflection.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ArmorDeflectedDMG_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    minDamageBlockedByPlayerArmorThisTurn = 1,
                    oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Shot mitigated.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ArmorDeflectedDMG_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    minDamageBlockedByPlayerArmorThisTurn = 1,
                    oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
                    oncePerCombat = true,
                    dialogue =
                    [
                        new(AmEvilCat, "smartass", "Yep. The armor is definitely our weakspot.")
                    ]
                }
            },

            //Dialogue for when the enemy and player ship don't overlap
            {
                "EvilCat_Dialogue_NoOverlap_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    shipsDontOverlapAtAll = true,
                    nonePresent = [ "crab", "scrap" ],
                    oncePerCombat = true,
                    oncePerCombatTags = [ "NoOverlapBetweenShips" ],
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "Can't hit us now!")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_NoOverlap_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    shipsDontOverlapAtAll = true,
                    nonePresent = [ "crab", "scrap" ],
                    oncePerCombat = true,
                    oncePerCombatTags = [ "NoOverlapBetweenShips" ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "We've shaken them for now.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_NoOverlap_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    shipsDontOverlapAtAll = true,
                    nonePresent = [ "crab", "scrap" ],
                    oncePerCombat = true,
                    oncePerCombatTags = [ "NoOverlapBetweenShips" ],
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "We aren't leaving, are we?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_NoOverlap_3", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    shipsDontOverlapAtAll = true,
                    nonePresent = [ "crab", "scrap" ],
                    oncePerCombat = true,
                    oncePerCombatTags = [ "NoOverlapBetweenShips" ],
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "Outmaneuvered.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_NoOverlap_4", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    shipsDontOverlapAtAll = true,
                    nonePresent = [ "crab", "scrap" ],
                    oncePerCombat = true,
                    oncePerCombatTags = [ "NoOverlapBetweenShips" ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Evasive manuevers successful.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_NoOverlapButSeeker_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    shipsDontOverlapAtAll = true,
                    doesNotHaveArtifacts = ["ChaffEmitters"],
                    oncePerCombatTags = [ "NoOverlapBetweenShipsSeeker"],
                    anyDronesHostile = [ "missile_seeker" ],
                    nonePresent = [ "crab" ],
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "That seeker is going to be a problem.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_NoOverlapButSeeker_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    shipsDontOverlapAtAll = true,
                    doesNotHaveArtifacts = ["ChaffEmitters"],
                    oncePerCombatTags = [ "NoOverlapBetweenShipsSeeker"],
                    anyDronesHostile = [ "missile_seeker" ],
                    nonePresent = [ "crab" ],
                    dialogue =
                    [
                        new(AmEvilCat, "grumpy", "Can we do something about that seeker?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeHaveNoWarpPrep_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    oncePerRun = true,
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    oncePerRunTags = [ "ShieldPrepIsGoneYouFool" ],
                    doesNotHaveArtifacts = [ "ShieldPrep", "WarpMastery"],
                    dialogue =
                    [
                        new(AmEvilCat, "grumpy", "Why did we leave Warp Prep behind?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedSimplicity_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    oncePerRun = true,
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    oncePerRunTags = [ "SimplicityShouts" ],
                    hasArtifacts = [ "Simplicity" ],
                    dialogue =
                    [
                        new(AmEvilCat, "smartass", "Now we simply destroy them.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedFractureDetection_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "FractureDetection" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "feral", "Shall we find that brittle spot?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeAreOnGemini_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    nonePresent = [ AmCat ],
                    hasArtifacts = [ "GeminiCore" ],
                    oncePerRunTags = [ "GeminiCore" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "Why can't I control more than one side at a time?")
                    ]
                }
            },
            {
                "EvilCatANDCat_Dialogue_WeAreOnGemini_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat, AmCat ],
                    hasArtifacts = [ "GeminiCore" ],
                    oncePerRunTags = [ "GeminiCore" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmCat, "Want to control the red side while I control the blue one?"),
                        new(AmEvilCat, "neutral", "Great idea!")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeAreOnJupiter_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    nonePresent = [ AmIsaac ],
                    hasArtifacts = [ "JupiterDroneHub" ],
                    oncePerRunTags = [ "JupiterDroneHub" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "smartass", "Why shoot with cannons when drones can do that for you?")
                    ]
                }
            },
            {
                "EvilCatANDIsaac_Dialogue_WeAreOnJupiter_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat, AmIsaac ],
                    hasArtifacts = [ "JupiterDroneHub" ],
                    oncePerRunTags = [ "JupiterDroneHub" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "smartass", "Why shoot with cannons when drones can do that for you?"),
                        new(AmIsaac, "sly", "...and what if they did the shooting automatically?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeAreOnTide_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "TideRunner" ],
                    oncePerRunTags = [ "TideRunner" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "grumpy", "I hate not being able to properly control this ship.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeAreOnAres_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "AresCannon" ],
                    oncePerRunTags = [ "AresCannon" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "Full control over the cannons? Perfect.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_CockpitLockon_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "CockpitTarget" ],
                    enemyHasPart = "cockpit",
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "feral", "Destroy their cockpit.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_CockpitLockonUseless_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "CockpitTarget" ],
                    enemyDoesNotHavePart = "cockpit",
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "grumpy", "No cockpit?")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedCleoGlasses_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    oncePerRun = true,
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    hasArtifacts = [ "BrokenGlasses" ],
                    dialogue =
                    [
                        new(AmEvilCat, "feral", "Cleo always put up a good fight.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookHealableChip_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    hasArtifacts = [ "NanofiberHull" ],
                    oncePerRun = true,
                    minDamageDealtToPlayerThisTurn = 1,
                    maxDamageDealtToPlayerThisTurn = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "Glad we have nanofibers.")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_CantHealThisChip_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    hasArtifacts = [ "NanofiberHull" ],
                    oncePerRun = true,
                    minDamageDealtToPlayerThisTurn = 2,
                    maxDamageDealtToPlayerThisTurn = 10,
                    dialogue =
                    [
                        new(AmEvilCat, "grumpy", "Nanofibers can't keep up with this!")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedCrosslink_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "Crosslink" ],
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "Crosslink is a great excuse to keep shooting!")
                    ]
                }
            },

            //ADD dialogue for:
            //nanofibers

        });
    }
}

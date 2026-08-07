using EvilCat.External;
using FMOD;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using static EvilCat.Conversation.CommonDefinitions;
using static OneOf.Types.TrueFalseOrNull;

namespace EvilCat.Conversation;

internal class EvilCatCombatDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {

            //Dialogue for player hitting a shot
            //remember to use <c=ff96f3></c> to highlight text
            //like the game does for void cat
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Shot hit.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Hit connected.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Got 'em!</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Pretty good, right?</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Damage confirmed.</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>Got you.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Opening exploited.</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Not bad, huh?</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>This isn't so hard!</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Easy peasy.</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>That was satisfying.</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>Afraid of us yet?</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>They're going to dodge left.</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>This'll be tricky...</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>Don't let them escape!</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>They're going to dodge right.</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>This'll be tricky.</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>Don't let them escape!</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Shot missed.</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>Why shoot empty space?</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>That was off.</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Totally calculated.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Recalibrating!</c>")
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
                    minCardsPlayedThisTurn = 8,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Still keeping up?</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ManyCards_1", new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = ["EvilCatManyCardsPlayed"],
                    oncePerCombat = true,
                    minCardsPlayedThisTurn = 8,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Wow, we're strong.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_ManyCards_2", new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = ["EvilCatManyCardsPlayed"],
                    oncePerCombat = true,
                    minCardsPlayedThisTurn = 8,
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "<c=ff96f3>We're good, aren't we?</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>This is useless.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>...This might be my fault.</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>Guess that's that.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Nothing left?</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Armor deflection.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Shot mitigated.</c>")
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
                        new(AmEvilCat, "smartass", "<c=ff96f3>Yep. Keep shooting our armor.</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Can't hit us now!</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>We lost them...for now.</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>We aren't leaving, are we?</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Outmanuevered.</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Evasive manuevers successful.</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>That seeker is a problem.</c>")
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
                        new(AmEvilCat, "grumpy", "<c=ff96f3>You didn't forget about the seeker, did you?</c>")
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
                        new(AmEvilCat, "grumpy", "<c=ff96f3>Why did we leave Warp Prep behind?</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedWarpMastery_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    nonePresent = [ AmMax ],
                    hasArtifacts = [ "WarpMastery" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "<c=ff96f3>Warp: mastered.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedWarpMasteryWITHMAX_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat, AmMax ],
                    hasArtifacts = [ "WarpMastery" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "<c=ff96f3>Warp: mastered.</c>"),
                        new(AmMax, "blush", "Did you just copy me?")
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
                        new(AmEvilCat, "smartass", "<c=ff96f3>Now we simply destroy them.</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>Shall we find that brittle spot?</c>")
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
                        new(AmEvilCat, "squint", "<c=ff96f3>Why can't I control more than one side at a time?</c>")
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
                        new(AmEvilCat, "neutral", "<c=ff96f3>Great idea!</c>")
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
                        new(AmEvilCat, "smartass", "<c=ff96f3>Why shoot with cannons when drones can do that for you?</c>")
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
                        new(AmEvilCat, "smartass", "<c=ff96f3>Why shoot with cannons when drones can do that for you?</c>"),
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
                        new(AmEvilCat, "grumpy", "<c=ff96f3>I hate trying to control this ship.</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Full control over the cannons? Perfect.</c>")
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
                        new(AmEvilCat, "angry", "<c=ff96f3>Destroy their cockpit!</c>")
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
                        new(AmEvilCat, "grumpy", "<c=ff96f3>No cockpit?</c>")
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
                        new(AmEvilCat, "feral", "<c=ff96f3>Cleo always put up a good fight.</c>")
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
                    oncePerCombatTags = ["EvilCat_Nanofibers_Comment"],
                    oncePerRun = true,
                    minDamageDealtToPlayerThisTurn = 1,
                    maxDamageDealtToPlayerThisTurn = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Good thing for the nanofibers.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookBoostedHealableChip_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    hasArtifacts = [ "NanofiberHull", "HealBooster" ],
                    oncePerCombatTags = ["EvilCat_Nanofibers_Comment"],
                    oncePerRun = true,
                    minDamageDealtToPlayerThisTurn = 2,
                    maxDamageDealtToPlayerThisTurn = 6,
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "<c=ff96f3>I can't hear you over these boosted nanofibers.</c>")
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
                    oncePerCombatTags = ["EvilCat_Nanofibers_Comment"],
                    oncePerRun = true,
                    minDamageDealtToPlayerThisTurn = 3,
                    maxDamageDealtToPlayerThisTurn = 10,
                    dialogue =
                    [
                        new(AmEvilCat, "grumpy", "<c=ff96f3>Nanofibers aren't enough for this!</c>")
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
                        new(AmEvilCat, "smug", "<c=ff96f3>Crosslink is a great excuse to keep shooting!</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_WeGainedEnergyPrep_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "EnergyPrep" ],
                    turnStart = true,
                    maxTurnsThisCombat = 1,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Batteries active.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_OverclockRunover_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    lookup = [ "OverclockedGeneratorTrigger" ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Auxiliary power active.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_IonConverterActivated_0", new()
                {
                    type = NodeType.combat,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    hasArtifacts = [ "IonConverter" ],
                    oncePerRunTags = [ "IonConverterTag" ],
                    lookup = [ "IonConverterTrigger" ],
                    priority = true,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Ion converter active!</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_OverheatGeneric_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    goingToOverheat = true,
                    oncePerCombatTags = ["OverheatGeneric"],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Ship emergency venting active!</c>")
                    ]
                }
            },
            {
                "WeJustGainedHeatAndDrakeIsHere_Multi_0", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "grumpy", "<c=ff96f3>That heat is burning our CPUs.</c>"),
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookChipDMG_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    doesNotHaveArtifacts = [ "NanofiberHull" ],
                    oncePerCombatTags = ["EvilCatYappedAboutDMG"],
                    minDamageDealtToPlayerThisTurn = 1,
                    maxDamageDealtToPlayerThisTurn = 2,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Minor hull breach detected.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookChipDMG_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    doesNotHaveArtifacts = [ "NanofiberHull" ],
                    oncePerCombatTags = ["EvilCatYappedAboutDMG"],
                    minDamageDealtToPlayerThisTurn = 1,
                    maxDamageDealtToPlayerThisTurn = 2,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Fixable damage, at least.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookChipDMG_2", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    doesNotHaveArtifacts = [ "NanofiberHull" ],
                    oncePerCombatTags = ["EvilCatYappedAboutDMG"],
                    minDamageDealtToPlayerThisTurn = 1,
                    maxDamageDealtToPlayerThisTurn = 2,
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>No major damage.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookMajorDMG_0", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    doesNotHaveArtifacts = [ "NanofiberHull" ],
                    oncePerCombatTags = ["EvilCatYappedAboutDMG"],
                    minDamageDealtToPlayerThisTurn = 3,
                    maxDamageDealtToPlayerThisTurn = 5,
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Major hull breach detected!</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_TookMajorDMG_1", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    enemyShotJustHit = true,
                    doesNotHaveArtifacts = [ "NanofiberHull" ],
                    oncePerCombatTags = ["EvilCatYappedAboutDMG"],
                    minDamageDealtToPlayerThisTurn = 3,
                    maxDamageDealtToPlayerThisTurn = 5,
                    dialogue =
                    [
                        new(AmEvilCat, "angry", "<c=ff96f3>I'll make them pay for that.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_BooksMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["booksWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingBooks],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Books?</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_CatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["CatWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingCat],
                    dialogue =
                    [
                        new(AmEvilCat, "smug", "<c=ff96f3>Don't worry - I'm still here.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_DizzyMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["dizzyWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingDizzy],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Where did Dizzy go?</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_DrakeMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["drakeWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingDrake],
                    dialogue =
                    [
                        new(AmEvilCat, "squint", "<c=ff96f3>Drake isn't at her station.</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_GoatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["issacWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingIsaac],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Isaac's vitals just vanished!</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_MaxMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["maxWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingMax],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>...Max?!</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_PeriMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["periWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingPeri],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Where did our security officer go?</c>")
                    ]
                }
            },
            {
                "EvilCat_Dialogue_RiggsMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmEvilCat ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["riggsWentMissing"],
                    lastTurnPlayerStatuses = [Status.missingRiggs],
                    dialogue =
                    [
                        new(AmEvilCat, "worried", "<c=ff96f3>Where is our pilot?</c>")
                    ]
                }
            },
            {
                "Riggs_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmRiggs ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmRiggs, "squint", "Is our computer supposed to do that?")
                    ]
                }
            },
            {
                "Peri_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmPeri ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmPeri, "squint", "CAT just uploaded herself to the other ship.")
                    ]
                }
            },
            {
                "Dizzy_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmDizzy ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmDizzy, "intense", "Rogue AIs are pretty scary, huh?")
                    ]
                }
            },
            {
                "Isaac_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmIsaac ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmIsaac, "shy", "...It's normal for her to do that, right?")
                    ]
                }
            },
            {
                "Max_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmMax ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmMax, "intense", "I did not program her to do that.")
                    ]
                }
            },
            {
                "Books_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmBooks ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmBooks, "plan", "Take em out while CAT distracts them!")
                    ]
                }
            },
            {
                "Drake_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmDrake ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmDrake, "...You know, taking control of their ship is pretty cool.")
                    ]
                }
            },
            {
                "Cat_Dialogue_EvilCatMissing", new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmCat ],
                    nonePresent = [ AmWizard ],
                    priority = true,
                    oncePerRun = true,
                    oncePerCombatTags = ["EvilCatWentMissing"],
                    lastTurnPlayerStatuses = [MissingEvilCat],
                    dialogue =
                    [
                        new(AmCat, "intense", "...I can do that?")
                    ]
                }
            },


        });
    }
}

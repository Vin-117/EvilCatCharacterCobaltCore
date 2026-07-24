using EvilCat.Actions;
using EvilCat.Artifacts;
using EvilCat.Cards;
using EvilCat.Conversation;
using EvilCat.External;
using EvilCat.Features;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Microsoft.Xna.Framework.Input;
using Nanoray.PluginManager;
using Nickel;
using Nickel;
using Nickel.Common;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static HarmonyLib.Code;

namespace EvilCat;



///
/// Set up ModEntry class
///
internal class ModEntry : SimpleMod
{



    ///
    /// Construct ModEntry, Harmony, and Kokoro instances
    ///
    internal static ModEntry Instance { get; private set; } = null!;
    internal Harmony Harmony;
    internal IKokoroApi.IV2 KokoroApi;
    public IModHelper helper { get; }



    ///
    /// Construct deck
    ///
    internal IDeckEntry EvilCatDeck;
    internal IDeckEntry FAKEEvilCatDeck;



    ///
    /// Construct character
    ///
    internal static IPlayableCharacterEntryV2 EvilCatPlayableCharacter { get; private set; } = null!;
    //internal static IPlayableCharacterEntryV2 FAKEEvilCatPlayableCharacter { get; private set; } = null!;

    public SingleLocalizationProvider? FakeDescription { get; init; }



    ///
    /// Construct status variables
    ///
    internal IStatusEntry EvilCatFullMemoryAccessStatus;
    internal IStatusEntry EvilCatMemoryMismatchStatus;
    internal IStatusEntry EvilCatGenericDrawStatus;
    internal IStatusEntry EvilCatDeallocateStatus;
    internal IStatusEntry EvilCatTempShieldExhaustStatus;



    ///
    /// This is required for dialogue machine registration
    ///
    public LocalDB localDB { get; set; } = null!;



    ///
    /// Construct localization
    ///
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }



    ///
    ///Define custom trait
    ///
    internal ICardTraitEntry EvilCatImmortalTrait;
    internal ICardTraitEntry TEMPEvilCatImmortalTrait;
    internal ISpriteEntry EvilCatImmortalIcon;


    ///
    /// Define card types as static lists
    ///
    private static List<Type> EvilCatCommonCardTypes = 
    [
        typeof(EvilCatCorrupt),
        typeof(EvilCatThrusterOverride),
        typeof(EvilCatViralTendency),
        typeof(EvilCatInstability),
        typeof(EvilCatSystemRefactor),
        typeof(EvilCatProcessSafeguard),
        typeof(EvilCatRestore),
        typeof(EvilCatAggressiveMode),
        typeof(EvilCatThreadTerminate)
    ];
    private static List<Type> EvilCatUncommonCardTypes = 
    [
        typeof(EvilCatFork),
        typeof(EvilCatAccessViolation),
        typeof(EvilCatHotReload),
        typeof(EvilCatSoftwareDaemon),
        typeof(EvilCatInfect),
        typeof(EvilCatSabotage),
        typeof(EvilCatHardReboot)
    ];
    private static List<Type> EvilCatRareCardTypes = 
    [
        typeof(EvilCatTrojan),
        typeof(EvilCatMemoryProtection),
        typeof(EvilCatMemoryMismatch),
        typeof(EvilCatDeallocate),
        typeof(EvilCatErase)
    ];
    private static List<Type> EvilCatSpecialCardTypes = 
    [
        typeof(EvilCatSegFault),
        typeof(EvilCatIfElse)
    ];
    private static List<Type> EvilCatEXECardTypes =
    [
        typeof(EvilCatEXECard)
    ];
    private static IEnumerable<Type> EvilCatCardTypes =
        EvilCatCommonCardTypes
            .Concat(EvilCatUncommonCardTypes)
            .Concat(EvilCatRareCardTypes)
            .Concat(EvilCatSpecialCardTypes)
            .Concat(EvilCatEXECardTypes);



    ///
    /// Define artifact types as static lists
    ///
    private static List<Type> EvilCatCommonArtifacts = 
    [    
        typeof(EvilCatBackupData),
        typeof(EvilCatBinaryExecution),
        typeof(EvilCatDangerousUnlock)
    ];
    private static List<Type> EvilCatBossArtifacts = 
    [    
        typeof(EvilCatVoidControl),
        typeof(EvilCatFullKernel),
        typeof(EvilCatBadMemory)
    ];
    private static IEnumerable<Type> EvilCatArtifactTypes =
        EvilCatCommonArtifacts
            .Concat(EvilCatBossArtifacts);



    ///
    /// Define dialogue types as static lists
    ///
    private static List<Type> EvilCatDialogueTypes =
    [
        typeof(EvilCatCombatDialogue),
        typeof(EvilCatEventDialogue)
    ];



    ///
    /// Collect all registerable types into one master list
    ///
    private static IEnumerable<Type> AllRegisterableTypes =
        EvilCatCardTypes
            .Concat(EvilCatArtifactTypes)
            .Concat(EvilCatDialogueTypes);



    ///
    /// Set up ModEntry helper
    ///
    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {



        ///
        /// Define instance, as well as Harmony and KokoroAPI fields
        ///
        Instance = this;
        this.helper = helper;
        Harmony = new Harmony("Vintage.EvilCat");
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2;



        ///
        /// Define localization lists
        ///
        AnyLocalizations = new JsonLocalizationProvider
        (
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>
        (
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );
        FakeDescription = AnyLocalizations.Bind(["character", "descFAKE"]).Localize;

        Harmony.PatchAll(typeof(ModEntry).Assembly);



        ///
        /// Define deck metadata
        ///
        EvilCatDeck = helper.Content.Decks.RegisterDeck("EvilCat", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                color = new Color("FF48EB"),
                titleColor = new Color("000000")
            },

            DefaultCardArt = StableSpr.cards_colorless,
            BorderSprite = RegisterSprite(package, "assets/EvilCat_CardBorder.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "name"]).Localize
        });



        ///
        /// Define fake deck metadata for a hacky workaround
        ///
        FAKEEvilCatDeck = helper.Content.Decks.RegisterDeck("FAKEEvilCat", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                color = new Color("FF48EB"),
                titleColor = new Color("000000")
            },

            DefaultCardArt = StableSpr.cards_colorless,
            BorderSprite = RegisterSprite(package, "assets/EvilCat_CardBorder.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "fakename"]).Localize
        });



        ///
        /// Define character metadata
        ///
        EvilCatPlayableCharacter = helper.Content.Characters.V2.RegisterPlayableCharacter("EvilCat", new PlayableCharacterConfigurationV2
        {
            Deck = EvilCatDeck.Deck,
            BorderSprite = RegisterSprite(package, "assets/EvilCat_CharacterBorder.png").Sprite,
            Starters = new StarterDeck
            {
                cards =
                [
                    new EvilCatCorrupt(),
                    new EvilCatThrusterOverride()
                ],
            },
            SoloStarters = new StarterDeck
            {
                cards =
                [
                    new EvilCatCorrupt(),
                    new EvilCatThrusterOverride(),
                    new EvilCatSystemRefactor(),
                    new EvilCatThreadTerminate(),
                    new BasicShieldColorless(),
                    new CannonColorless()
                ]
            },
            ExeCardType = typeof(EvilCatEXECard),
            Description = AnyLocalizations.Bind(["character", "desc"]).Localize
        });


        ///
        /// Define alternate starting cards for the more difficulties mod
        /// as well as starters for custom run option duos
        ///
        helper.ModRegistry.AwaitApi<IMoreDifficultiesApi>(
            "TheJazMaster.MoreDifficulties",
            new SemanticVersion(1, 3, 0),
            api => api.RegisterAltStarters
            (
                deck: EvilCatDeck.Deck,
                starterDeck: new StarterDeck
                {
                    cards =
                    [
                        new EvilCatSystemRefactor(),
                        new EvilCatThreadTerminate(),
                    ]
                }

            )
        );
        helper.ModRegistry.AwaitApi<ICustomRunOptionsApi>("Shockah.CustomRunOptions", cro =>
        {
            cro.RegisterPartialDuoDeck(EvilCatDeck.Deck, new StarterDeck
            {
                cards =
                [
                    new EvilCatCorrupt(),
                    new EvilCatThrusterOverride(),
                    new EvilCatThreadTerminate(),
                ]
            });
        });



        ///
        /// Define status metadata and manager
        ///
        EvilCatFullMemoryAccessStatus = helper.Content.Statuses.RegisterStatus("EvilCatFullMemoryAccessStatus", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = false,
                color = new Color("9E3DFF"),
                icon = RegisterSprite(package, "assets/Status/OptimizerStatus.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "EvilCatFullMemoryAccessStatus", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "EvilCatFullMemoryAccessStatus", "desc"]).Localize
        });
        _ = new EvilCatFullMemoryAccessStatusManager();

        EvilCatMemoryMismatchStatus = helper.Content.Statuses.RegisterStatus("EvilCatMemoryMismatchStatus", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = false,
                color = new Color("7F7F7F"),
                icon = RegisterSprite(package, "assets/Status/BufferOverflow.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "EvilCatMemoryMismatchStatus", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "EvilCatMemoryMismatchStatus", "desc"]).Localize
        });
        _ = new EvilCatMemoryMismatchStatusManager();

        EvilCatGenericDrawStatus = helper.Content.Statuses.RegisterStatus("EvilCatGenericDrawStatus", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = false,
                color = new Color("3FBFFF"),
                icon = RegisterSprite(package, "assets/Status/GenericDraw.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "EvilCatGenericDrawStatus", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "EvilCatGenericDrawStatus", "desc"]).Localize
        });
        _ = new EvilCatGenericDrawStatusManager();

        EvilCatDeallocateStatus = helper.Content.Statuses.RegisterStatus("EvilCatDeallocateStatus", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = false,
                color = new Color("D3A8FF"),
                icon = RegisterSprite(package, "assets/Status/Deallocate.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "EvilCatDeallocateStatus", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "EvilCatDeallocateStatus", "desc"]).Localize
        });
        _ = new EvilCatDeallocateStatusManager();

        EvilCatTempShieldExhaustStatus = helper.Content.Statuses.RegisterStatus("EvilCatTempShieldExhaustStatus", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = false,
                color = new Color("D3A8FF"),
                icon = RegisterSprite(package, "assets/Status/tempShieldExhaust.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "EvilCatTempShieldExhaustStatus", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "EvilCatTempShieldExhaustStatus", "desc"]).Localize
        });
        _ = new EvilCatTempShieldExhaustStatusManager();


        ///
        ///Define tooltip sprites
        ///
        AExhaustSelect.AExhaustSelectSpr = RegisterSprite(package, "assets/Actions/chooseExhaust.png").Sprite;
        AOptionalExhaustSelect.AOptionalExhaustSelectSpr = RegisterSprite(package, "assets/Actions/optionalExhaust.png").Sprite;



        ///
        ///Define trait sprites
        ///
        EvilCatImmortalIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardTrait/Immortal.png"));


        ///
        ///Define custom traits
        ///
        EvilCatImmortalTrait = helper.Content.Cards.RegisterTrait("ImmortalTrait", new()
        {
            Name = this.AnyLocalizations.Bind(["trait", "Immortal", "name"]).Localize,
            Icon = (state, card) => EvilCatImmortalIcon.Sprite,
            Tooltips = (state, card) => 
            [
                new GlossaryTooltip($"trait.{Instance.Package.Manifest.UniqueName}::Immortal")
                {
                    Icon = EvilCatImmortalIcon.Sprite,
                    TitleColor = Colors.cardtrait,
                    Title = Localizations.Localize(["trait", "Immortal", "name"]),
                    Description = Localizations.Localize(["trait", "Immortal", "desc"])
                },
            ]
        });
        _ = new ImmortalTraitManager();

        ///Need to make a temporary version of the immortal trait for Registry Edit
        TEMPEvilCatImmortalTrait = helper.Content.Cards.RegisterTrait("TEMPImmortalTrait", new()
        {
            Name = this.AnyLocalizations.Bind(["trait", "Immortal", "name"]).Localize,
            Icon = (state, card) => EvilCatImmortalIcon.Sprite,
            Tooltips = (state, card) =>
            [
                new GlossaryTooltip($"trait.{Instance.Package.Manifest.UniqueName}::Immortal")
                {
                    Icon = EvilCatImmortalIcon.Sprite,
                    TitleColor = Colors.cardtrait,
                    Title = Localizations.Localize(["trait", "Immortal", "name"]),
                    Description = Localizations.Localize(["trait", "Immortal", "desc"])
                },
            ]
        });
        _ = new TEMPImmortalTraitManager();



        ///
        /// Define gameover and mini sprites, which require slightly different implementations
        ///
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = EvilCatDeck.Deck.Key(),
            LoopTag = "gameover",
            Frames = [
                RegisterSprite(package, "assets/Animation/EvilCat_GameOver.png").Sprite,
            ]
        });
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = EvilCatDeck.Deck.Key(),
            LoopTag = "mini",
            Frames = [
                RegisterSprite(package, "assets/Animation/EvilCat_Mini.png").Sprite,
            ]
        });
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = FAKEEvilCatDeck.Deck.Key(),
            LoopTag = "mini",
            Frames = [
                RegisterSprite(package, "assets/Animation/EvilCat_Mini.png").Sprite,
            ]
        });



        ///
        /// Define all other sprites
        ///
        RegisterAnimation(package, "neutral", "assets/Animation/Neutral/EvilCat_Neutral", 4);
        RegisterAnimation(package, "squint", "assets/Animation/Squint/EvilCat_Squint", 4);
        RegisterAnimation(package, "feral", "assets/Animation/Feral/EvilCat_Feral", 4);
        RegisterAnimation(package, "angry", "assets/Animation/Angry/EvilCat_Angry", 4);
        RegisterAnimation(package, "smug", "assets/Animation/Smug/EvilCat_Smug", 4);
        RegisterAnimation(package, "smartass", "assets/Animation/Smartass/EvilCat_Smartass", 4);
        RegisterAnimation(package, "grumpy", "assets/Animation/Grumpy/EvilCat_Grumpy", 4);
        RegisterAnimation(package, "furious", "assets/Animation/Furious/EvilCat_Furious", 4);



        ///
        ///Define some fake sprites so the game doesn't crash
        ///
        RegisterFakeAnimation(package, "neutral", "assets/Animation/Neutral/EvilCat_Neutral", 4);
        RegisterFakeAnimation(package, "squint", "assets/Animation/Squint/EvilCat_Squint", 4);

        ///
        /// Initialize all cards and artifacts defined by static lists
        /// THIS MUST BE DONE JUST BEFORE DIALOGUE MACHINE, IDEALLY AT THE BOTTOM
        ///
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);



        ///
        /// Setup dialogue machine localdB
        ///
        helper.Events.OnModLoadPhaseFinished += (_, phase) =>
        {
            if (phase == ModLoadPhase.AfterDbInit)
            {
                localDB = new(helper, package);
            }
        };
        helper.Events.OnLoadStringsForLocale += (_, thing) =>
        {
            foreach (KeyValuePair<string, string> entry in localDB.GetLocalizationResults(thing.Locale))
            {
                thing.Localizations[entry.Key] = entry.Value;
            }
        };



        ///
        ///Define patch to change Evil Cat's name to proper display name in combat
        ///
        var ResetName_PatchTargetMethod = typeof(Character).GetMethod("GetDisplayName", AccessTools.all, new[] { typeof(string), typeof(State) });
        var ResetName_PatchInsertionMethod = typeof(ModEntry).GetMethod("ResetDisplayName", AccessTools.all);
        Harmony.Patch(ResetName_PatchTargetMethod, postfix: new HarmonyMethod(ResetName_PatchInsertionMethod));



        ///
        ///Define patch to replace CAT in upper left corner with EVIL CAT when she is in the crew
        ///
        var RenderEvilCat_PatchTargetMethod = typeof(Character).GetMethod("RenderComputer", AccessTools.all);
        var RenderEvilCat_PatchInsertionMethod = typeof(ModEntry).GetMethod("RenderEvilCat", AccessTools.all);
        Harmony.Patch(RenderEvilCat_PatchTargetMethod, prefix: new HarmonyMethod(RenderEvilCat_PatchInsertionMethod));


        //var RenderEvilCatDesc_PatchTargetMethod = typeof(Tooltips).GetMethod("Render", AccessTools.all);
        //var RenderEvilCatDesc_PatchInsertionMethod = typeof(ModEntry).GetMethod("RenderEvilCatDesc", AccessTools.all);
        //Harmony.Patch(RenderEvilCatDesc_PatchTargetMethod, postfix: new HarmonyMethod(RenderEvilCatDesc_PatchInsertionMethod));



        ///
        ///Define patch to give functionality to Full Memory Access and Deallocate
        ///
        var EvilCatFullMemoryAccess_Deallocate_patch_target = typeof(Combat).GetMethod("SendCardToExhaust", AccessTools.all);
        var EvilCatFullMemoryAccess_Deallocate_patch_insert = typeof(ModEntry).GetMethod("EvilCatFullMemoryAccessPatch", AccessTools.all);
        Harmony.Patch(EvilCatFullMemoryAccess_Deallocate_patch_target, postfix: new HarmonyMethod(EvilCatFullMemoryAccess_Deallocate_patch_insert));

    }



    ///
    /// Define method for easier sprite registration
    ///
    public static ISpriteEntry RegisterSprite(IPluginPackage<IModManifest> package, string dir)
    {
        return Instance.Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile(dir));
    }



    ///
    /// Define method for easier animation registration
    ///
    public static void RegisterAnimation(IPluginPackage<IModManifest> package, string tag, string dir, int frames)
    {
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Instance.EvilCatDeck.Deck.Key(),
            LoopTag = tag,
            Frames = Enumerable.Range(0, frames)
                .Select(i => RegisterSprite(package, dir + i + ".png").Sprite)
                .ToImmutableList()
        });
    }


    ///
    /// Register fake animations for fake version of evil cat (used for hacky workaround)
    ///
    public static void RegisterFakeAnimation(IPluginPackage<IModManifest> package, string tag, string dir, int frames)
    {
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Instance.FAKEEvilCatDeck.Deck.Key(),
            LoopTag = tag,
            Frames = Enumerable.Range(0, frames)
                .Select(i => RegisterSprite(package, dir + i + ".png").Sprite)
                .ToImmutableList()
        });
    }



    ///
    ///Define method to change the way Evil Cat's name is displayed in combat
    /// 
    private static void ResetDisplayName(ref string __result)
    {
        //NOTE: This breaks if another mod has a name starting with "CAT?"
        //OR if using different languages. If either of these edge cases 
        //ever come up will need to change this.
        if (__result == "CAT?")
        {
            __result = "\\\\cat.msi";
        }
    }



    ///
    ///Define method to replace CAT with EVIL CAT if she is in the player's crew
    ///
    public static bool RenderEvilCat(G g, Vec offset, bool showDialogue = true) 
    {

        bool evilCatInCrew = false;

        foreach (Character character in g.state.characters)
        {
            if (character.deckType == Instance.EvilCatDeck.Deck)
            {
                evilCatInCrew = true;
            }
        }

        //Skip this method if evil cat not in crew
        if (!evilCatInCrew) 
        {
            return true;
        }


        Character obj = new Character
        {
            type = "Vintage.EvilCat::FAKEEvilCat",
            deckType = Instance.FAKEEvilCatDeck.Deck,
            artifacts = g.state.artifacts
        };
        int x = (int)offset.x;
        int y = (int)offset.y;
        bool showDialogue2 = showDialogue;
        obj.Render(g, x, y, flipX: false, mini: true, isExploding: false, isDoneExploding: false, isEscaping: false, null, renderLocked: false, hideFace: false, showUnlockInstructions: false, showDialogue2);



        return false;

    }



    ///
    ///Define method to give functionality to Full Memory Access and Deallocate
    ///
    private static void EvilCatFullMemoryAccessPatch(State s, Card card, ref Combat __instance)
    {
        Status statusFullMemoryAccess = ModEntry.Instance.EvilCatFullMemoryAccessStatus.Status;
        Status statusDeallocate = ModEntry.Instance.EvilCatDeallocateStatus.Status;
        Status statusTempShieldExhaust = ModEntry.Instance.EvilCatTempShieldExhaustStatus.Status;

        if (s.ship.Get(statusFullMemoryAccess) > 0)
        {
            __instance.QueueImmediate 
            (

                new ADrawCard() 
                {
                    count = s.ship.Get(statusFullMemoryAccess)
                }
            );
        }

        if (s.ship.Get(statusTempShieldExhaust) > 0)
        {
            __instance.QueueImmediate
            (

                new AStatus()
                {
                    statusAmount = s.ship.Get(statusTempShieldExhaust),
                    targetPlayer = true,
                    status = Status.tempShield
                }
            );
        }

        if (s.ship.Get(statusDeallocate) > 0)
        {
            __instance.QueueImmediate
            (
                new AStatus()
                {
                    statusAmount = s.ship.Get(statusDeallocate),
                    targetPlayer = true,
                    status = Status.evade
                }
            );
        }

    }

}


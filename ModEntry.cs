using EvilCat.Actions;
using EvilCat.Cards;
using EvilCat.External;
using EvilCat.Features;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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



    ///
    /// Construct deck
    ///
    internal IDeckEntry EvilCatDeck;



    ///
    /// Construct character
    ///
    internal static IPlayableCharacterEntryV2 EvilCatPlayableCharacter { get; private set; } = null!;



    ///
    /// Construct status variables
    ///
    //internal IStatusEntry PLACEHOLDER;



    ///
    /// This is required for dialogue machine registration
    ///
    //public LocalDB localDB { get; set; } = null!;



    ///
    /// Construct localization
    ///
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }



    ///
    ///Define custom trait
    ///
    internal ICardTraitEntry EvilCatImmortalTrait;
    internal ISpriteEntry EvilCatImmortalIcon;


    ///
    /// Define card types as static lists
    ///
    private static List<Type> EvilCatCommonCardTypes = 
    [
        typeof(EvilCatCorrupt),
        typeof(EvilCatDisplace),
        typeof(EvilCatFester)
    ];
    private static List<Type> EvilCatUncommonCardTypes = 
    [    
    ];
    private static List<Type> EvilCatRareCardTypes = 
    [    
    ];
    private static List<Type> EvilCatSpecialCardTypes = 
    [
        typeof(EvilCatVoid)
    ];
    private static List<Type> EvilCatEXECardTypes =
    [
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
    ];
    private static List<Type> EvilCatBossArtifacts = 
    [    
    ];
    private static IEnumerable<Type> EvilCatArtifactTypes =
        EvilCatCommonArtifacts
            .Concat(EvilCatBossArtifacts);



    ///
    /// Define dialogue types as static lists
    ///
    private static List<Type> EvilCatDialogueTypes =
    [
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
                    new EvilCatDisplace(),
                    new EvilCatFester()
                ],
            },
            SoloStarters = new StarterDeck
            {
                cards = 
                [
                ]
            },
            //ExeCardType = typeof(PLACEHOLDER),
            Description = AnyLocalizations.Bind(["character", "desc"]).Localize
        });



        ///
        /// Define alternate starting cards for the more difficulties mod
        /// as well as starters for custom run option duos
        ///
        //(TODO)



        ///
        /// Define status metadata and manager
        ///
        //(TODO)


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
                new GlossaryTooltip($"trait.{Instance.Package.Manifest.UniqueName}::Sequential")
                {
                    Icon = EvilCatImmortalIcon.Sprite,
                    TitleColor = Colors.cardtrait,
                    Title = Localizations.Localize(["trait", "Immortal", "name"]),
                    Description = Localizations.Localize(["trait", "Immortal", "desc"])
                },
            ]
        });



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



        ///
        /// Define all other sprites
        ///
        RegisterAnimation(package, "neutral", "assets/Animation/Neutral/EvilCat_Neutral", 4);
        RegisterAnimation(package, "squint", "assets/Animation/Squint/EvilCat_Squint", 3);



        ///
        /// Initialize all cards and artifacts defined by static lists
        /// THIS MUST BE DONE JUST BEFORE DIALOGUE MACHINE, IDEALLY AT THE BOTTOM
        ///
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);



        //
        //Define patch to change Evil Cat's name to proper display name in combat
        //
        var ResetName_PatchTargetMethod = typeof(Character).GetMethod("GetDisplayName", AccessTools.all, new[] { typeof(string), typeof(State) });
        var ResetName_PatchInsertionMethod = typeof(ModEntry).GetMethod("ResetDisplayName", AccessTools.all);
        Harmony.Patch(ResetName_PatchTargetMethod, postfix: new HarmonyMethod(ResetName_PatchInsertionMethod));



        ///
        /// Setup dialogue machine localdB
        ///
        //(TODO)



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



    //
    //Define method to change the way Evil Cat's name is displayed in combat
    //
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

}


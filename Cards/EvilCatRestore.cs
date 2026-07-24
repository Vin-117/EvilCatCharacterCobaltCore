using EvilCat.Actions;
using EvilCat.Features;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatRestore : Card, IRegisterable, IHasCustomCardTraits
{
    //
    //Begin card registration
    //
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            //
            //Define card metadata
            //
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.EvilCatDeck.Deck,
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },



            //
            //Define card name and art file
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatRestore", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/Circut.png")).Sprite,
        });
    }



    //
    //Define card cost and traits for default and each upgrade path
    //
    public override CardData GetData(State state)
    {
        switch (this.upgrade)
        {
            case Upgrade.None:
                {
                    return new CardData
                    {
                        cost = 0,
                        exhaust = true,
                        description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "EvilCatRestore", "desc"]))
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 0,
                        exhaust = true,
                        description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "EvilCatRestore", "descA"]))
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 1,
                        description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "EvilCatRestore", "descB"]))
                    };
                }
            default:
                {
                    return new CardData{};
                }
        }
    }



    ///
    /// Define additional custom card traits
    ///
    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
    {
        switch (this.upgrade)
        {
            case Upgrade.None:
                {
                    return new HashSet<ICardTraitEntry> { };
                }
            case Upgrade.A:
                {
                    return new HashSet<ICardTraitEntry> { };
                }
            case Upgrade.B:
                {
                    this.SetIsImmortal(true);
                    HashSet<ICardTraitEntry> cardTraitEntries = new HashSet<ICardTraitEntry>()
                    {
                        ModEntry.Instance.EvilCatImmortalTrait
                    };
                    return cardTraitEntries;
                }
            default:
                {
                    return new HashSet<ICardTraitEntry> { };
                }
        }

    }

    //
    //Define what actions the card performs for default and each upgrade path
    //
    public override List<CardAction> GetActions(State s, Combat c)
    {
        switch (this.upgrade)
        {
            case Upgrade.None:
                {
                    return new List<CardAction>
                    {
                        new ACardSelect
                        {
                            browseAction = new ReanimatePickCard{ },
                            browseSource = CardBrowse.Source.ExhaustPile,
                            ignoreCardType = new EvilCatRestore().Key()
                        }
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new ACardSelect
                        {
                            browseAction = new ReanimatePickCard{ },
                            browseSource = CardBrowse.Source.ExhaustPile,
                            ignoreCardType = new EvilCatRestore().Key()
                        },
                        new ACardSelect
                        {
                            browseAction = new ReanimatePickCard{ },
                            browseSource = CardBrowse.Source.ExhaustPile,
                            ignoreCardType = new EvilCatRestore().Key()
                        }
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        new ACardSelect
                        {
                            browseAction = new ReanimatePickCardToDiscard{ },
                            browseSource = CardBrowse.Source.ExhaustPile,
                            ignoreCardType = new EvilCatRestore().Key()
                        }
                    };
                }
            default:
                {
                    return new List<CardAction>{};
                }
        }
    }
}
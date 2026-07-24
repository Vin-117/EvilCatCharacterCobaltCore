using EvilCat.Actions;
using EvilCat.Features;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;
using EvilCat.Actions;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatErase : Card, IRegisterable
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
                rarity = Rarity.rare,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },



            //
            //Define card name and art file
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatErase", "name"]).Localize,
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
                        buoyant = true,
                        description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "EvilCatErase", "desc"]))
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 0,
                        exhaust = true,
                        buoyant = true,
                        description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "EvilCatErase", "descA"]))
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 0,
                        exhaust = true,
                        buoyant = true,
                        description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "EvilCatErase", "descB"]))
                    };
                }
            default:
                {
                    return new CardData{};
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
                            browseSource = CardBrowse.Source.DrawPile,
                            browseAction = new EvilCatxhaustCardBrowseAction
                            {
                                OnSuccess = []
                            }
                        },
                        new ADrawCard
                        {
                            count = 1
                        }
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new ACardSelect
                        {
                            browseSource = CardBrowse.Source.DrawPile,
                            browseAction = new EvilCatxhaustCardBrowseAction
                            {
                                OnSuccess = []
                            }
                        },
                        new ADrawCard
                        {
                            count = 2
                        }
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        new ACardSelect
                        {
                            browseSource = CardBrowse.Source.DrawPile,
                            browseAction = new EvilCatxhaustCardBrowseAction
                            {
                                OnSuccess = []
                            }
                        },
                        new ADrawCard
                        {
                            count = 1
                        },
                        new AEnergy
                        {
                            changeAmount = 1
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
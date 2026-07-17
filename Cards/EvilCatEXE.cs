using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;
using EvilCat.Actions;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatEXECard : Card, IRegisterable
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
                deck = Deck.colorless,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },



            //
            //Define card name and art file
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatEXECard", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/plain.png")).Sprite,
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
                        cost = 1,
                        exhaust = true,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatEXECard", "desc"])
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 0,
                        exhaust = true,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatEXECard", "descA"])
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 1,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatEXECard", "descB"])
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
                        new ACardOffering
                        {
                            amount = 3,
                            limitDeck = ModEntry.Instance.EvilCatDeck.Deck,
                            makeAllCardsTemporary = true,
                            overrideUpgradeChances = false,
                            canSkip = false,
                            inCombat = true,
                            discount = -1,
                        },
                        new AExhaustSelect
                        {
                            count = 1
                        }
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new ACardOffering
                        {
                            amount = 3,
                            limitDeck = ModEntry.Instance.EvilCatDeck.Deck,
                            makeAllCardsTemporary = true,
                            overrideUpgradeChances = false,
                            canSkip = false,
                            inCombat = true,
                            discount = -1,
                        },
                        new AExhaustSelect
                        {
                            count = 1
                        }
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        new ACardOffering
                        {
                            amount = 5,
                            limitDeck = ModEntry.Instance.EvilCatDeck.Deck,
                            makeAllCardsTemporary = true,
                            overrideUpgradeChances = false,
                            canSkip = false,
                            inCombat = true,
                            discount = -1,
                        },
                        new AExhaustSelect
                        {
                            count = 1
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
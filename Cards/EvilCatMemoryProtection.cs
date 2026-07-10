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
public class EvilCatMemoryProtection : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatMemoryProtection", "name"]).Localize,
            //Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/FILENAME.png")).Sprite,
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
                        cost = 2,
                        exhaust = true
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 1,
                        exhaust = true,
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 2,
                        unplayable = true
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
                        new AStatus
                        {
                            targetPlayer = true,
                            statusAmount = 1,
                            status = ModEntry.Instance.EvilCatTempShieldExhaustStatus.Status
                        }
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new AStatus
                        {
                            targetPlayer = true,
                            statusAmount = 1,
                            status = ModEntry.Instance.EvilCatTempShieldExhaustStatus.Status
                        }

                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                targetPlayer = true,
                                statusAmount = 1,
                                status = ModEntry.Instance.EvilCatTempShieldExhaustStatus.Status
                            }
                        ).AsCardAction

                    };
                }
            default:
                {
                    return new List<CardAction>{};
                }
        }
    }
}
using EvilCat.Actions;
using EvilCat.External;
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
public class EvilCatSabotage : Card, IRegisterable
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
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },



            //
            //Define card name and art file
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatSabotage", "name"]).Localize,
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
                        cost = 1,
                        exhaust = false
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 1,
                        exhaust = false
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 1,
                        exhaust = true
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
                        new AExhaustSelect
                        {
                            count = 1
                        },
                        new AStatus
                        {
                            statusAmount = 1,
                            targetPlayer = false,
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive
                        }
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {

                        new AExhaustSelect
                        {
                            count = 1
                        },
                        new AStatus
                        {
                            statusAmount = 1,
                            targetPlayer = false,
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive
                        },
                        new AStatus
                        {
                            statusAmount = 2,
                            targetPlayer = false,
                            status = Status.backwardsMissiles
                        }

                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {

                        new AOptionalExhaustSelect
                        {
                            count = 2
                        },
                        new AStatus
                        {
                            statusAmount = 2,
                            targetPlayer = false,
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive
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
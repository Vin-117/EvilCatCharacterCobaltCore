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
public class EvilCatTrojan : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatTrojan", "name"]).Localize,
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
                        cost = 3,
                        exhaust = true
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 3,
                        exhaust = true
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 3,
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

                        new AStatus()
                        {
                            status = ModEntry.EvilCatPlayableCharacter.MissingStatus.Status,
                            statusAmount = 1,
                            targetPlayer = true
                        },
                        new AMove
                        {
                            targetPlayer = false,
                            isRandom = true,
                            dir = -6
                        },
                        new AStatus
                        {
                            status = Status.heat,
                            statusAmount = 6,
                            targetPlayer = false,
                        },
                        new AStatus
                        {
                            status = Status.corrode,
                            statusAmount = 1,
                            targetPlayer = false,
                        }
                        
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {

                        new AStatus()
                        {
                            status = ModEntry.EvilCatPlayableCharacter.MissingStatus.Status,
                            statusAmount = 1,
                            targetPlayer = true
                        },
                        new AMove
                        {
                            targetPlayer = false,
                            isRandom = true,
                            dir = -6
                        },
                        new AStatus
                        {
                            status = Status.heat,
                            statusAmount = 6,
                            targetPlayer = false,
                        },
                        new AStatus
                        {
                            status = Status.corrode,
                            statusAmount = 1,
                            targetPlayer = false,
                        },
                        new AStatus
                        {
                            status = Status.backwardsMissiles,
                            statusAmount = 5,
                            targetPlayer = false,
                        }

                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {

                        new AStatus()
                        {
                            status = ModEntry.EvilCatPlayableCharacter.MissingStatus.Status,
                            statusAmount = 1,
                            targetPlayer = true
                        },
                        new AMove
                        {
                            targetPlayer = false,
                            isRandom = true,
                            dir = -6
                        },
                        new AStatus
                        {
                            status = Status.corrode,
                            statusAmount = 2,
                            targetPlayer = false,
                        },

                    };
                }
            default:
                {
                    return new List<CardAction>{};
                }
        }
    }
}
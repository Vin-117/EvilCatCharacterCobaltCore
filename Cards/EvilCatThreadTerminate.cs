using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;
using EvilCat.Actions;
using System.Linq.Expressions;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatThreadTerminate : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatThreadTerminate", "name"]).Localize,
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
                        cost = 1
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 0
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 1
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
                        new AAttack
                        {
                            damage = GetDmg(s, 2),
                            piercing = true,
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive,
                            statusAmount = 1
                        },
                        new AAddCard
                        {
                            card = new EvilCatSegFault()
                            {
                            },
                            destination = CardDestination.Discard,
                            amount = 1,
                        }

                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new AAttack
                        {
                            damage = GetDmg(s, 2),
                            piercing = true,
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive,
                            statusAmount = 1
                        },
                        new AAddCard
                        {
                            card = new EvilCatSegFault()
                            {
                            },
                            destination = CardDestination.Discard,
                            amount = 1,
                        }

                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {

                        new AStatus
                        {
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive,
                            statusAmount = 1,
                            targetPlayer = false
                        },
                        new AAttack
                        {
                            damage = GetDmg(s, 2),
                            piercing = true,
                            stunEnemy = true
                        },
                        new AAddCard
                        {
                            card = new EvilCatSegFault()
                            {
                            },
                            destination = CardDestination.Discard,
                            amount = 1,
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
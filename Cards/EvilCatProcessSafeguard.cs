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
public class EvilCatProcessSafeguard : Card, IRegisterable, IHasCustomCardTraits
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatProcessSafeguard", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/Access Denied.png")).Sprite,
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
                        cost = 1
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 0
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
                    this.SetIsImmortal(true);
                    HashSet<ICardTraitEntry> cardTraitEntries = new HashSet<ICardTraitEntry>()
                    {
                        ModEntry.Instance.EvilCatImmortalTrait
                    };
                    return cardTraitEntries;
                }
            case Upgrade.A:
                {
                    this.SetIsImmortal(true);
                    HashSet<ICardTraitEntry> cardTraitEntries = new HashSet<ICardTraitEntry>()
                    {
                        ModEntry.Instance.EvilCatImmortalTrait
                    };
                    return cardTraitEntries;
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

                        new AStatus
                        {
                            targetPlayer = true,
                            status = Status.tempShield,
                            statusAmount = 2
                        },
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.tempShield,
                                statusAmount = 2,
                                targetPlayer = true
                            }
                        ).AsCardAction

                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {

                        new AStatus
                        {
                            targetPlayer = true,
                            status = Status.tempShield,
                            statusAmount = 3
                        },
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.tempShield,
                                statusAmount = 3,
                                targetPlayer = true
                            }
                        ).AsCardAction

                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {

                        new AStatus
                        {
                            targetPlayer = true,
                            status = Status.tempShield,
                            statusAmount = 1
                        },
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.tempShield,
                                statusAmount = 2,
                                targetPlayer = true
                            }
                        ).AsCardAction,
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.tempPayback,
                                statusAmount = 2,
                                targetPlayer = true
                            }
                        ).AsCardAction,



                    };
                }
            default:
                {
                    return new List<CardAction>{};
                }
        }
    }
}
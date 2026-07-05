using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;
using static EvilCat.External.IKokoroApi.IV2;
using EvilCat.Features;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatViralTendency : Card, IRegisterable, IHasCustomCardTraits
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatViralTendency", "name"]).Localize,
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
                        cost = 2
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 2,
                        retain = true
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
                            status = Status.overdrive,
                            statusAmount = 1,
                            targetPlayer = true
                        },
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.overdrive,
                                statusAmount = 1,
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
                            status = Status.overdrive,
                            statusAmount = 1,
                            targetPlayer = true
                        },
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.overdrive,
                                statusAmount = 1,
                                targetPlayer = true
                            }
                        ).AsCardAction

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
                                status = Status.timeStop,
                                statusAmount = 1,
                                targetPlayer = true
                            }
                        ).AsCardAction,
                        ModEntry.Instance.KokoroApi.OnExhaust.MakeAction
                        (
                            new AStatus
                            {
                                status = Status.overdrive,
                                statusAmount = 1,
                                targetPlayer = true
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
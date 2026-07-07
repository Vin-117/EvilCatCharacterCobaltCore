using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;
using EvilCat.Actions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatFork : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatFork", "name"]).Localize,
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
                        retain = true,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatFork", "desc"], new { cnt = GetDmg(state, 2) })
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 1,
                        retain = true,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatFork", "descA"], new { cnt = GetDmg(state, 2) })
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 0,
                        retain = true,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatFork", "descB"], new { cnt = GetDmg(state, 2) })
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
                            piercing = false
                        },
                        new AAddCard
                        {
                            card = new EvilCatFork
                            {
                                temporaryOverride = true,
                            },
                            destination = CardDestination.Discard
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
                            piercing = true
                        },
                        new AAddCard
                        {
                            card = new EvilCatFork
                            {
                                temporaryOverride = true,
                                upgrade = Upgrade.A
                            },
                            destination = CardDestination.Discard
                        }

                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {

                        new AAttack
                        {
                            damage = GetDmg(s, 1),
                            piercing = false
                        },
                        new AAddCard
                        {
                            card = new EvilCatFork
                            {
                                temporaryOverride = true,
                                upgrade = Upgrade.B
                            },
                            destination = CardDestination.Discard
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
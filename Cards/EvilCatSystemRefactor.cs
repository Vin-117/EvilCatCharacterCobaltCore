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
public class EvilCatSystemRefactor : Card, IRegisterable
{

    private static ISpriteEntry SystemRefactorArtNormal = null!;
    private static ISpriteEntry SystemRefactorArtFlipped = null!;


    //
    //Begin card registration
    //
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {

        SystemRefactorArtNormal = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/EvilCatSystemRefactor.png"));
        SystemRefactorArtFlipped = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/EvilCatSystemRefactorFlipped.png"));

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
            //Define card name
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatSystemRefactor", "name"]).Localize,
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
                        art = flipped ? SystemRefactorArtFlipped.Sprite : SystemRefactorArtNormal.Sprite
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 2,
                        flippable = true,
                        art = flipped ? SystemRefactorArtFlipped.Sprite : SystemRefactorArtNormal.Sprite
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 2,
                        art = flipped ? SystemRefactorArtFlipped.Sprite : SystemRefactorArtNormal.Sprite
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
                        new AMove
                        {
                            dir = -2,
                            targetPlayer = false
                        },
                        new AMove
                        {
                            dir = 1,
                            targetPlayer = true
                        },
                        new AOptionalExhaustSelect
                        {
                            count = 1
                        }
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new AMove
                        {
                            dir = -2,
                            targetPlayer = false
                        },
                        new AMove
                        {
                            dir = 1,
                            targetPlayer = true
                        },
                        new AOptionalExhaustSelect
                        {
                            count = 1
                        }
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        new AMove
                        {
                            dir = -2,
                            targetPlayer = false
                        },
                        new AMove
                        {
                            dir = 2,
                            targetPlayer = true
                        },
                        new AOptionalExhaustSelect
                        {
                            count = 2
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
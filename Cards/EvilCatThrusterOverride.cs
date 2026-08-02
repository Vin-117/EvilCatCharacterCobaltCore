using EvilCat.Actions;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EvilCat.Cards;


//
//Define card unique class
//
public class EvilCatThrusterOverride : Card, IRegisterable
{

    private static ISpriteEntry ThrusterOverrideArtNormal = null!;
    private static ISpriteEntry ThrusterOverrideArtFlipped = null!;


    //
    //Begin card registration
    //
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {

        ThrusterOverrideArtNormal = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/EvilCatThrusterOverride.png"));
        ThrusterOverrideArtFlipped = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/EvilCatThrusterOverrideFlipped.png"));

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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatThrusterOverride", "name"]).Localize,
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
                        flippable = true,
                        art = flipped ? ThrusterOverrideArtFlipped.Sprite : ThrusterOverrideArtNormal.Sprite
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 0,
                        flippable = true,
                        art = flipped ? ThrusterOverrideArtFlipped.Sprite : ThrusterOverrideArtNormal.Sprite
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 1,
                        flippable = true,
                        retain = true,
                        art = flipped ? ThrusterOverrideArtFlipped.Sprite : ThrusterOverrideArtNormal.Sprite
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
                            dir = -3,
                            targetPlayer = false
                        },
                        new AAddSegFault
                        {
                            amount = 1
                        },
                        
                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new AMove
                        {
                            dir = -3,
                            targetPlayer = false
                        },
                        new AAddSegFault
                        {
                            amount = 1
                        },
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        new AMove
                        {
                            dir = -3,
                            targetPlayer = false
                        },
                        new AAddSegFault
                        {
                            amount = 1
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
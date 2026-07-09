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
public class EvilCatIfElse : Card, IRegisterable
{

    //Define variables for different splits for the dual card
    private static ISpriteEntry TopArt = null!;
    private static ISpriteEntry BottomArt = null!;

    //
    //Begin card registration
    //
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {

        //Define art for top and bottom of dual card
        TopArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/IfElseTop.png"));
        BottomArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Card/IfElseBottom.png"));


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
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },



            //
            //Define card name and art file
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatIfElse", "name"]).Localize,
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
                        cost = 0,
                        retain = true,
                        exhaust = true,
                        temporary = true,
                        floppable = true,
                        art = (flipped ? BottomArt : TopArt).Sprite
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 0,
                        retain = true,
                        temporary = true,
                        floppable = true,
                        art = (flipped ? BottomArt : TopArt).Sprite
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 0,
                        retain = true,
                        exhaust = true,
                        temporary = true,
                        floppable = true,
                        art = (flipped ? BottomArt : TopArt).Sprite
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
                            status = Status.overdrive,
                            statusAmount = 1,
                            targetPlayer = true,
                            disabled = flipped
                        },
                        new ADummyAction(),
                        new AStatus
                        {
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive,
                            statusAmount = 1,
                            targetPlayer = false,
                            disabled = !flipped
                        },
                        
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
                            targetPlayer = true,
                            disabled = flipped
                        },
                        new ADummyAction(),
                        new AStatus
                        {
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive,
                            statusAmount = 1,
                            targetPlayer = false,
                            disabled = !flipped
                        },
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {
                        new AStatus
                        {
                            status = Status.overdrive,
                            statusAmount = 2,
                            targetPlayer = true,
                            disabled = flipped
                        },
                        new ADummyAction(),
                        new AStatus
                        {
                            status = ModEntry.Instance.KokoroApi.DriveStatus.Underdrive,
                            statusAmount = 2,
                            targetPlayer = false,
                            disabled = !flipped
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
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
public class EvilCatFullMemoryAccess : Card, IRegisterable
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
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },



            //
            //Define card name and art file
            //
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvilCatFullMemoryAccess", "name"]).Localize,
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
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatFullMemoryAccess", "desc"])
                    };
                }
            case Upgrade.A:
                {
                    return new CardData
                    {
                        cost = 1,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatFullMemoryAccess", "descA"])
                    };
                }
            case Upgrade.B:
                {
                    return new CardData
                    {
                        cost = 2,
                        retain = true,
                        description = ModEntry.Instance.Localizations.Localize(["card", "EvilCatFullMemoryAccess", "descB"])
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

                        new ACardSelect
                        {
                            browseAction = new AFullKernelAccessToDiscard(),
                            browseSource = CardBrowse.Source.ExhaustPile
                        }

                    };
                }
            case Upgrade.A:
                {
                    return new List<CardAction>
                    {
                        new ACardSelect
                        {
                            browseAction = new AFullKernelAccessToDiscard(),
                            browseSource = CardBrowse.Source.ExhaustPile
                        }
                    };
                }
            case Upgrade.B:
                {
                    return new List<CardAction>
                    {

                        new ACardSelect
                        {
                            browseAction = new AFullKernelAccessToDiscard(),
                            browseSource = CardBrowse.Source.ExhaustPile
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
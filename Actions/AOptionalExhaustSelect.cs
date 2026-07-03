using EvilCat.Actions;
using EvilCat.External;
using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace EvilCat.Actions;



///
/// Function which prompts the user to select a set number of cards from their hand to discard. Discarding is not optional.
///
public class AOptionalExhaustSelect : CardAction
{
    public required int count; //User must pass number of cards that must be exhausted
    public static Spr AOptionalExhaustSelectSpr; //Define icon var
    public CardDestination source = CardDestination.Hand; //Define source to select from (hand)
    public CardDestination destination = CardDestination.Exhaust; //Define where to send cards (exhaust pile)

    public override Route? BeginWithRoute(G g, State s, Combat c)
    {
        //Setup card browse action
        CardBrowse cardBrowse = new CardBrowse
        {
            mode = CardBrowse.Mode.Browse,
            browseSource = Source(source),
            browseAction = new AMultiBrowseExhaustActions { },
            allowCancel = false
        };

        //Do nothing if there is nothing in source pile
        if (cardBrowse.GetCardList(g).Count == 0)
        {
            return null;
        }
        //count = Math.Min(count, cardBrowse.GetCardList(g).Count);

        //Define browse route
        var multiBrowseRoute = ModEntry.Instance.KokoroApi.MultiCardBrowse.MakeRoute(cardBrowse);

        //Player MUST exhaust number of cards equal to count, where possible
        multiBrowseRoute.MaxSelected = count;
        multiBrowseRoute.MinSelected = 0;

        timer = 0.0; //Make this happen instantly

        return multiBrowseRoute.AsRoute;
    }



    /// 
    /// Define icon displayed inside the card
    /// 
    public override Icon? GetIcon(State s)
    {
        return new Icon
        {
            path = AOptionalExhaustSelectSpr,
            number = count,
            color = Colors.textMain
        };
    }



    ///
    /// Define icon and tooltip when hovering over a card with this action 
    ///
    public override List<Tooltip> GetTooltips(State s)
    {

        if (count == 1)
        {
            return
            [
                new GlossaryTooltip(key: "AOptionalExhaustSelect")
                {
                    Icon = AOptionalExhaustSelectSpr,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "AOptionalExhaustSelect", "title"]),
                    TitleColor = Colors.card,
                    Description = ModEntry.Instance.Localizations.Localize(["action", "AOptionalExhaustSelect", "desc_single"])
                },
            ];
        }
        else 
        {
            return
            [
                new GlossaryTooltip(key: "AOptionalExhaustSelect")
                {
                    Icon = AOptionalExhaustSelectSpr,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "AOptionalExhaustSelect", "title"]),
                    TitleColor = Colors.card,
                    Description = ModEntry.Instance.Localizations.Localize(["action", "AOptionalExhaustSelect", "desc"], new { cnt = count })
                },
            ];
        }
    }



    ///
    /// Function which is used to compress a switch statement
    /// 
    public static CardBrowse.Source Source(CardDestination mode)
    {
        return mode switch
        {
            CardDestination.Discard => CardBrowse.Source.DiscardPile,
            CardDestination.Exhaust => CardBrowse.Source.ExhaustPile,
            CardDestination.Hand => CardBrowse.Source.Hand,
            CardDestination.Deck => CardBrowse.Source.DrawPile,
            //CardDestination.DrawOrDiscardPile => CardBrowse.Source.DrawOrDiscardPile,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}
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
/// Function which prompts the user to select a set number of cards from their draw pile to exhaust. Exhausting is not optional.
///
public class AExhaustDrawSelect : CardAction
{
    public required int count; //User must pass number of cards that must be exhausted

    public override Route? BeginWithRoute(G g, State s, Combat c)
    {
        //Setup card browse action
        CardBrowse cardBrowse = new CardBrowse
        {
            mode = CardBrowse.Mode.Browse,
            browseSource = CardBrowse.Source.DrawPile,
            browseAction = new AMultiBrowseExhaustActions { count = count, fromDraw = true },
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
        multiBrowseRoute.MinSelected = count;

        timer = 0.0; //Make this happen instantly

        return multiBrowseRoute.AsRoute;
    }
}
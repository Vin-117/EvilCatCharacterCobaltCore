using daisyowl.text;
using EvilCat.Actions;
using EvilCat.External;
using EvilCat.Features;
using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace EvilCat.Actions;






// UK.VARIABLE -> Enum.Parse<UK>("VARIABLE")

public class ShowCardsStrFix : Route, OnMouseDown
{
    public List<int> cardIds = new List<int>();

    public required string messageKey;

    public void OnMouseDown(G g, Box b)
    {
        if (b.key == Enum.Parse<UK>("shipUpgrades_continue"))
        {
            Audio.Play(Event.Click);
            g.CloseRoute(this);
        }
    }

    public override void Render(G g)
    {
        G g2 = g;
        List<Card> list = (from cid in cardIds
                           select g2.state.FindCard(cid) into c
                           where c != null
                           select c).ToList();
        CardUtils.FanOut(list, new Vec(240.0, 90.0));
        foreach (Card item in list)
        {
            item.UpdateAnimation(g2);
        }

        Draw.Sprite(StableSpr.cockpit_deletionChamber, 0.0, 0.0);
        Draw.Fill(Colors.redd.gain(Mutil.Remap(-1.0, 1.0, 0.05, 0.1, Math.Sin(g2.state.time * 4.0))), BlendMode.Add);
        string str = messageKey;
        Color? color = Colors.textBold;
        TAlign? align = TAlign.Center;
        Color? outline = Colors.black;
        Draw.Text(str, 240.0, 69.0, null, color, null, null, null, align, dontDraw: false, null, outline);
        SharedArt.ButtonText(g2, new Vec(210.0, 193.0), Enum.Parse<UK>("shipUpgrades_continue"), Loc.T("uiShared.btnContinue"), null, null, inactive: false, this, null, null, null, null, autoFocus: true);
        foreach (Card item2 in list)
        {
            G g3 = g2;
            State fakeState = DB.fakeState;
            item2.Render(g3, null, fakeState);
        }
    }
}
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



///
/// Function which prompts the user to select a card to add the immortal trait to it.
///
public class AAddImmortal : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {

        Card card = selectedCard!; //Define variable to store selected card

        //If the card is NOT null
        if (card != null)
        {
            //If the card does not already have the immortal trait
            if (!ImmortalTraitExt.GetIsImmortal(card)) 
            {
                ModEntry.Instance.helper.Content.Cards.SetCardTraitOverride(s, card, ModEntry.Instance.EvilCatImmortalTrait, true, false);
                ImmortalTraitExt.SetIsImmortal(card, true);
            }
        }


    }

}
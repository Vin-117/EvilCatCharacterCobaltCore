using System;
using System.Reflection.Metadata.Ecma335;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Graphics;

namespace EvilCat.Features;

/*public static class ChangeName
{
    public static void Apply(Harmony harmony)
    {

        harmony.Patch(
            original: typeof(Character).GetMethod(nameof(Character.GetDisplayName), AccessTools.all),
            prefix: new HarmonyMethod(typeof(ChangeName), nameof(ResetName))
        );
    }

    private static string ResetName(Character __instance, string charId, State state)
    {

        Console.WriteLine(__instance.type);

        string b = default(string)!;

        if (__instance.type == ModEntry.EvilCatPlayableCharacter.CharacterType)
        {
            //"\\\\cat.msi";
            b = "\\\\cat.msi";

        }

        return b;

    }


}*/
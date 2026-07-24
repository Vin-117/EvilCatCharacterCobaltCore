using HarmonyLib;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Input;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;

namespace EvilCat.Patches;

[HarmonyPatch(typeof(Character))] // This?
public static class EvilCatPatchDescription
{
    private static ModEntry Instance => ModEntry.Instance;

    [HarmonyTranspiler]
    [HarmonyPatch("Render")]
    private static IEnumerable<CodeInstruction> EvilCatPatchDescription_Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase originalMethod)
    {
        try
        {
            return new SequenceBlockMatcher<CodeInstruction>(instructions)
                .Find([
                    ILMatches.Ldarg(0).ExtractLabels(out var labels),
                    ILMatches.Ldfld("shout"),
                    ILMatches.Instruction(OpCodes.Dup),
                    ILMatches.Brtrue

                ])
                .Insert(SequenceMatcherPastBoundsDirection.Before, SequenceMatcherInsertionResultingBounds.IncludingInsertion, 
                [
                    new CodeInstruction(OpCodes.Ldarg_1).WithLabels(labels),
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.DeclaredField(typeof(Character), nameof(Character.type)) ),
                    new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(EvilCatPatchDescription), nameof(RenderEvilCatDesc)))
                    ])
                .AllElements();
        }
        catch (Exception ex)
        {
            ModEntry.Instance.Logger!.LogError("Could not patch method {DeclaringType}::{Method} - {Mod} probably won't work.\nReason: {Exception}", originalMethod.DeclaringType, originalMethod, ModEntry.Instance, ex);
            return instructions;
        }
        // ReSharper restore PossibleMultipleEnumeration
    }

    public static void RenderEvilCatDesc(G g, string type, Vec pos)
    {
        ModEntry.Instance.Logger!.LogInformation("Got here");
        if (type == "Vintage.EvilCat::FAKEEvilCat")
        {
            ModEntry.Instance.Logger!.LogInformation("Got here 2");
            g.tooltips.AddGlossary(pos, "TEST");
        }
    }


}
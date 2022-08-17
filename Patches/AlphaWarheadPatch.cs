using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CassieBreaker.Patches
{
    [HarmonyPatch(typeof(AlphaWarheadController))]
    public static class AlphaWarheadPatch
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AlphaWarheadController.CancelDetonation), new Type[]{typeof(GameObject)})]
        public static IEnumerable<CodeInstruction> CancelDetonationTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();

            newInstructions.InsertRange(101, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[110].labels.Add(ret);
            foreach (var instruction in newInstructions)
                yield return instruction;
        }
        
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AlphaWarheadController.StartDetonation))]
        public static IEnumerable<CodeInstruction> StartDetonationTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();

            newInstructions.InsertRange(63, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[67].labels.Add(ret);
            foreach (var instruction in newInstructions)
                yield return instruction;
        }
    }
}
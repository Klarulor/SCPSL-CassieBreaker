using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using Mirror;
using Respawning.NamingRules;
using Subtitles;
using Utils.Networking;

namespace CassieBreaker.Patches
{
    [HarmonyPatch(typeof(NineTailedFoxNamingRule), nameof(NineTailedFoxNamingRule.PlayEntranceAnnouncement))]
    public static class NineTailedFoxNamingRulePatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator){
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();
            //newInstructions.RemoveRange(168, 5);
            newInstructions.InsertRange(168, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[177].labels.Add(ret);
            foreach (var instruction in newInstructions)
                yield return instruction;
        }
    }
}
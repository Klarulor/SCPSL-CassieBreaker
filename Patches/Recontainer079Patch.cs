using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace CassieBreaker.Patches
{
    [HarmonyPatch(typeof(Recontainer079))]
    public class Recontainer079Patch
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Recontainer079.EndOvercharge))]
        public static IEnumerable<CodeInstruction> EndOverchargeTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();
            int index = instructions.ToList().FindIndex(x => x.opcode == OpCodes.Call && x.operand is MethodBase method && method.Name == "SendToAuthenticated");
            newInstructions.InsertRange(index, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[index + 4].labels.Add(ret);
            foreach (var instruction in newInstructions)
            {
                yield return instruction;
            }
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Recontainer079.Recontain))]
        public static IEnumerable<CodeInstruction> RecontainTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();
            int index = instructions.ToList().FindIndex(x => x.opcode == OpCodes.Call && x.operand is MethodBase method && method.Name == "SendToAuthenticated");
            newInstructions.InsertRange(index, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[index + 4].labels.Add(ret);
            foreach (var instruction in newInstructions)
            {
                yield return instruction;
            }
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Recontainer079.UpdateStatus))]
        public static IEnumerable<CodeInstruction> UpdateStatusTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();
            int index = instructions.ToList().FindIndex(x => x.opcode == OpCodes.Call && x.operand is MethodBase method && method.Name == "SendToAuthenticated");
            newInstructions.InsertRange(index, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[index + 4].labels.Add(ret);
            foreach (var instruction in newInstructions)
            {
                yield return instruction;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using LightContainmentZoneDecontamination;

namespace CassieBreaker.Patches
{
    [HarmonyPatch(typeof(DecontaminationController), nameof(DecontaminationController.UpdateTime))]
    public class DecontaminationControllerPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();

            newInstructions.InsertRange(151, new []
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.CassieLyricsEnabled))),
                new CodeInstruction(OpCodes.Brfalse_S, ret)
            });
            newInstructions[160].labels.Add(ret);
            foreach (var instruction in newInstructions)
                yield return instruction;
        }
    }
}
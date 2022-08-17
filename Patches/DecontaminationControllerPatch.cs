using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using LightContainmentZoneDecontamination;
using Mirror;
using Mono.Cecil;
using Subtitles;
using UnityEngine;

namespace CassieBreaker.Patches
{
    [HarmonyPatch(typeof(DecontaminationController), nameof(DecontaminationController.UpdateTime))]
    public class DecontaminationControllerPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            var ret = generator.DefineLabel();

            int index = instructions.ToList().FindIndex(instruction =>
                instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodBase methodBase &&
                methodBase.Name == "Send");

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
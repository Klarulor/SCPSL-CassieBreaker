using Respawning;

namespace CassieBreaker.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(RespawnEffectsController), "ServerPassCassie")]
    public static class RespawnEffectControllerPatch
    {
        public static bool Prefix(string words, bool makeHold, bool makeNoise, bool customAnnouncement)
        {
            return Plugin.Instance.Config.CassieSpeakingEnabled;
        }
    }
}
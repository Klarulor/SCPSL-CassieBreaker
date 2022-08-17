using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;

namespace CassieBreaker
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override PluginPriority Priority { get; } = PluginPriority.Last;
        public Plugin() => Instance = this;
        public string PluginName => typeof(Plugin).Namespace;
        private Harmony _harmony = new Harmony("CassieBreaker-patching");
        public override void OnEnabled()
        {
            _harmony.PatchAll();
            Log.Info($"Plugin {PluginName} started");
        }

        public override void OnDisabled()
        {
            
        }
        
    }
}
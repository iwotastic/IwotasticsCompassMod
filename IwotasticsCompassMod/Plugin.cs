using BepInEx;
using HarmonyLib;

namespace IwotasticsCompassMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} was loaded successfully.");

        Logger.LogInfo($"Patching HUDManager...");
        Harmony.CreateAndPatchAll(typeof(HUDManagerHooks));
        Logger.LogInfo($"Done!");
    }
}
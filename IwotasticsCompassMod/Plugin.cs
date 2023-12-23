using BepInEx;
using HarmonyLib;

namespace IwotasticsCompassMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} was loaded successfully.");

        Logger.LogInfo($"Patching compass into HUDManager...");
        Harmony.CreateAndPatchAll(typeof(HUDManagerHooks));
        Logger.LogInfo($"Finished patching HUDManager!");
    }
}
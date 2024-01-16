using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace IwotasticsCompassMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<float> CompassXpos;
    public static ConfigEntry<float> CompassYpos;

    private void Awake()
    {
        CompassXpos = Config.Bind<float>("General", "CompassXpos", 0f, "X coordinate of the compass position (0 means it's in the center of the screen)");
        CompassYpos = Config.Bind<float>("General", "CompassYpos", -235f, "Y coordinate of the compass position (0 means it's in the center of the screen)");

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} (v{MyPluginInfo.PLUGIN_VERSION}) was loaded successfully.");

        Logger.LogInfo($"Patching compass into HUDManager...");

        Harmony.CreateAndPatchAll(typeof(HUDManagerHooks));
        Logger.LogInfo($"Finished patching HUDManager!");
    }
}

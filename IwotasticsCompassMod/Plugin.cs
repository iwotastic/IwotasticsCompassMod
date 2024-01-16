using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace IwotasticsCompassMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<float> CompassPosX;
    public static ConfigEntry<float> CompassPosY;
    public static ConfigEntry<bool> ShouldMoveClockDown;

    private void Awake()
    {
        CompassPosX = Config.Bind<float>("General", "CompassPosX", 0f, "X coordinate of the compass position (0 means it's in the center of the screen)");
        CompassPosY = Config.Bind<float>("General", "CompassPosY", 185f, "Y coordinate of the compass position (0 means it's in the center of the screen)");
        ShouldMoveClockDown = Config.Bind<bool>("General", "ShouldMoveClockDown", true, "Push the clock down a little so that the compass doesn't clip over it.\n (automatically disabled if the position of the clock is not the default one)");

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} (v{MyPluginInfo.PLUGIN_VERSION}) was loaded successfully.");

        Logger.LogInfo($"Patching compass into HUDManager...");

        Harmony.CreateAndPatchAll(typeof(HUDManagerHooks));
        Logger.LogInfo($"Finished patching HUDManager!");
    }
}

using HarmonyLib;
using UnityEngine;

namespace IwotasticsCompassMod;

public class HUDManagerHooks
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "Start")]
    static void StartHook(ref HUDManager __instance)
    {
        // Create HUD game object once and parent it under the HUDContainer
        var hudCompassObject = new GameObject();
        hudCompassObject.transform.SetParent(__instance.HUDContainer.transform, false);
        var newCompassPos = new Vector3(Plugin.CompassXpos.Value, Plugin.CompassYpos.Value, 0f);
        hudCompassObject.transform.localPosition = newCompassPos;

        // Add our own script to the compass object, so it can do compass things
        var hudCompassBehaviour = hudCompassObject.AddComponent<HUDCompassBehaviour>();
       
        // This is blursed, but I can't think of a better way to get the font that the game uses
        hudCompassBehaviour.compassFontAsset =
            __instance.weightCounter == null ? null : __instance.weightCounter.font;
    }
}

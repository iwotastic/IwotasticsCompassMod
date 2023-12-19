using System.Collections;
using HarmonyLib;
using UnityEngine;
using System.Linq;

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
        var newPos = new Vector3(0f, 190f, 0f);
        hudCompassObject.transform.localPosition = newPos;

        // Add our own script to the compass object, so it can do compass things
        var hudCompassBehaviour = hudCompassObject.AddComponent<HUDCompassBehaviour>();
       
        // This is blursed, but I can't think of a better way to get the font that the game uses
        hudCompassBehaviour.fontToUse = __instance.weightCounter == null ? null : __instance.weightCounter.font;
    }
}
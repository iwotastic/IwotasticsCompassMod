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
        var newCompassPos = new Vector3(0f, 193f, 0f);
        hudCompassObject.transform.localPosition = newCompassPos;

        // Add our own script to the compass object, so it can do compass things
        var hudCompassBehaviour = hudCompassObject.AddComponent<HUDCompassBehaviour>();
       
        // This is blursed, but I can't think of a better way to get the font that the game uses
        hudCompassBehaviour.compassFontAsset =
            __instance.weightCounter == null ? null : __instance.weightCounter.font;
        
        // Push the clock down a little so that the compass doesn't clip over it anymore
        var clockTransform = __instance.Clock.canvasGroup.transform;
        var oldClockPos = clockTransform.localPosition;
        var newClockPos = new Vector3(oldClockPos.x, oldClockPos.y - 27, oldClockPos.z);
        Debug.Log($"Set clock position to {newClockPos}");
        clockTransform.localPosition = newClockPos;
    }
}
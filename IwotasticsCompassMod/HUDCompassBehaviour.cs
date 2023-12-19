using System;
using TMPro;
using UnityEngine;

namespace IwotasticsCompassMod;

public class HUDCompassBehaviour : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

    public TMP_FontAsset fontToUse;
    
    private const String COMPASS_STRING = "|--:--N--:--|--:--E--:--|--:--S--:--|--:--W--:--|--:--N--:--|";
    private const int COMPASS_WINDOW = 13;
    private const float COMPASS_STEP_SIZE = 360f / 48f;
    
    private void Start()
    {
        tmpText = gameObject.AddComponent<TextMeshProUGUI>();
        tmpText.color = Color.green;
        tmpText.fontSize = 24f;
        tmpText.alignment = TextAlignmentOptions.Center;
        if (fontToUse != null) tmpText.font = fontToUse;
        tmpText.text = "0deg";
    }

    private void Update()
    {
        // Owie my eyes...
        var cameraForward = GameNetworkManager.Instance.localPlayerController.turnCompassCamera.transform.forward;
        var x = cameraForward.x;
        var z = cameraForward.z;
        var heading = 360f - (Mathf.Atan2(z, x) * Mathf.Rad2Deg + 180f);

        var compassPos = Mathf.FloorToInt((heading + (COMPASS_STEP_SIZE / 2f)) / COMPASS_STEP_SIZE);
        tmpText.text = COMPASS_STRING.Substring(compassPos, COMPASS_WINDOW);
    }
}
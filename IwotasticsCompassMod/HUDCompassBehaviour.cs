using System;
using TMPro;
using UnityEngine;

namespace IwotasticsCompassMod;

public class HUDCompassBehaviour : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

    public TMP_FontAsset compassFontAsset;
    
    private const String COMPASS_STRING = "|--:--N--:--|--:--E--:--|--:--S--:--|--:--W--:--|--:--N--:--|";
    private const int COMPASS_WINDOW = 13;
    private const float COMPASS_STEP_SIZE = 360f / 48f;
    
    private void Start()
    {
        // Set up font settings
        tmpText = gameObject.AddComponent<TextMeshProUGUI>();
        tmpText.color = Color.green;
        tmpText.fontSize = 24f;
        tmpText.alignment = TextAlignmentOptions.Center;
        if (compassFontAsset != null) tmpText.font = compassFontAsset;
        
        Debug.Log("Compass loaded!");
    }

    private void Update()
    {
        // Owie my eyes...
        var cameraForward = GameNetworkManager.Instance.localPlayerController.turnCompassCamera.transform.forward;
        
        // Calculate the heading
        var x = cameraForward.x;
        var z = cameraForward.z;
        var heading = 360f - (Mathf.Atan2(z, x) * Mathf.Rad2Deg + 180f);

        // Calculate compass string position offset
        var compassPos = Mathf.FloorToInt((heading + (COMPASS_STEP_SIZE / 2f)) / COMPASS_STEP_SIZE);
        tmpText.text = COMPASS_STRING.Substring(compassPos, COMPASS_WINDOW);
    }
}

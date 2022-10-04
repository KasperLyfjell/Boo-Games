using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSettings : MonoBehaviour
{
    #region Runtime Variables
    public TextMeshProUGUI FPSOverlay;
    private float frames;
    #endregion


    #region Overlay
    public static bool FPScounter;
    #endregion

    #region Camera Settings
    [Range(0, 6)]
    public static float HeadBobbing = 3;//3 as default for testing
    [Range(1, 8)]
    public static float MouseSensitivity = 3;
    #endregion

    #region Sound
    #endregion

    private void Start()
    {
        if (FPScounter && FPSOverlay != null)
            FPSOverlay.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (FPSOverlay.gameObject.activeSelf && FPSOverlay != null)
        {
            frames = 1 / Time.deltaTime;
            frames = (int)frames;
        }
    }

    private void FixedUpdate()
    {
        if (FPSOverlay.gameObject.activeSelf && FPSOverlay != null)
        {
            FPSOverlay.text = frames.ToString();
        }
    }
}

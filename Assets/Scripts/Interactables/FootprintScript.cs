using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class FootprintScript : MonoBehaviour
{
    Light lanternLightColor;
    bool close = false;
    DecalProjector dp;

    private void Start()
    {
        lanternLightColor = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        dp = GetComponentInChildren<DecalProjector>();

        dp.fadeFactor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (close == true && lanternLightColor.color == Color.blue)
        {
            dp.fadeFactor = 1f;
        }
        else
        {
            dp.fadeFactor = 0f;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        close = true;
    }

    private void OnTriggerExit(Collider other)
    {
        close = false;
    }
}

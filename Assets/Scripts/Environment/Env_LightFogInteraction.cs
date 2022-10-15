using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Env_LightFogInteraction : MonoBehaviour
{
    public HDAdditionalLightData lighterData;
    public float lightMultiplier;
    public float lightMultiplierValue;

    public float lightMultiplierMax = 1f;
    public float lightMultiplierMin = 0.5f;

    public float transitionSpeed = 0.1f;



    private void Start()
    {
        lightMultiplier = lighterData.volumetricDimmer;
        lightMultiplier = lightMultiplierValue;
        lightMultiplierValue = lightMultiplierMax;
        lightMultiplier = lightMultiplierValue;

    }

    private void Update()
    {
        if (lightMultiplier != lightMultiplierValue)
        {
            if(lightMultiplierValue == lightMultiplierMin)
            {
                lightMultiplier -= transitionSpeed;
            }
            if (lightMultiplierValue == lightMultiplierMax)
            {
                lightMultiplier += transitionSpeed;
            }

        }

        if(lightMultiplier >=lightMultiplierMax || lightMultiplier <= lightMultiplierMin)
        {
            lightMultiplier = lightMultiplierValue;
        }

        lighterData.volumetricDimmer = lightMultiplier;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        lightMultiplierValue = lightMultiplierMin;
    }

    [ContextMenu("Revert")]
    public void Revert()
    {
        lightMultiplierValue = lightMultiplierMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        lightMultiplierValue = lightMultiplierMin;
    }

    private void OnTriggerExit(Collider other)
    {
        lightMultiplierValue = lightMultiplierMax;
    }
}

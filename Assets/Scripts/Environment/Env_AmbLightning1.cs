using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Env_AmbLightning1 : MonoBehaviour
{
    public HDAdditionalLightData lightningData;
    public GameObject lightning;

    public float lightStrengthMax = 40000f;
    public float lightStrengthMin = 0f;
    public float lightTransitionVar = 0.5f;

    public float lightningValue;
    public float transitionSpeed = 0.2f;

    float _t = 0.0f;

    public bool ActiveLightning;
    void Start()
    {
        lightningValue = lightStrengthMax;
    }

    
    void Update()
    {
        if (ActiveLightning)
        {
            lightning.SetActive(true);

            lightningValue = Mathf.Lerp(lightStrengthMax, lightStrengthMin, _t);
            _t += lightTransitionVar * Time.deltaTime;

            lightningData.intensity = lightningValue;


            StartCoroutine(Wait());
        }
        else
        {
            lightning.SetActive(false);
            lightningValue = lightStrengthMin;
            lightningData.intensity = lightningValue;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(transitionSpeed);
        ActiveLightning = false;
        lightningValue = lightStrengthMax;
        _t = 0;
    }

    public void activateStrike()
    {
        ActiveLightning = true;
    }
}

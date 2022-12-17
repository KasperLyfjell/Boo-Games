using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Env_AmbLightning : MonoBehaviour
{
    public HDAdditionalLightData lightningData;
    public GameObject lightning;
    public DynamicAudioZone dynAudio;
    //public float audioZoneFloat; 

    public float lightStrengthMax = 40000f;
    public float lightStrengthMin = 0f;
    public float lightTransitionVar = 0.5f;

    public float lightningValue;
    public float transitionSpeed = 0.2f;

    public bool ActiveLightning;
    public bool Test;
    void Start()
    {
        lightningValue = lightStrengthMax;

    }

    
    void Update()
    {
        float _t = 0.0f;

        if (dynAudio.IsInside == true || Test == true )
        {

            if(dynAudio.delay < 0.2) 
            //Sjekk om thunder delay er mindre enn 1 og sett til true
            //Spilles av pittelitt før lightning 
            {
                ActiveLightning = true;
            }


            if (ActiveLightning)
            {
                lightning.SetActive(true);
                lightningValue = lightStrengthMax;
                lightningData.intensity = lightningValue;

                lightningValue = Mathf.Lerp(lightStrengthMax, lightStrengthMin, _t);
                _t += lightTransitionVar * Time.deltaTime;

                StartCoroutine(Wait());
            }
            else
            {
                lightning.SetActive(false);
                lightningValue = lightStrengthMin;
                lightningData.intensity = lightningValue;
            }

        }
        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(transitionSpeed);
        ActiveLightning = false;
    }
}

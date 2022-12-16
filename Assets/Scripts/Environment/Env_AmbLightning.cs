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

    public float lightningValue;
    public float transitionSpeed = 0.2f;

    public bool ActiveLightning;
    void Start()
    {
        lightningValue = lightStrengthMax;

    }

    
    void Update()
    {
        if (dynAudio.IsInside == false)
        {

            if(dynAudio.delay < 0.2) 
            //Sjekk om thunder delay er mindre enn 1 og sett til true
            //Spilles av pittelitt f�r lightning 
            {
                ActiveLightning = true;
            }


            if (ActiveLightning)
            {
                lightning.SetActive(true);
                lightningValue = lightStrengthMax;
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
        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(transitionSpeed);
        ActiveLightning = false;
    }
}

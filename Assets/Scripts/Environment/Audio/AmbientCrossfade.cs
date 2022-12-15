using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientCrossfade : MonoBehaviour
{
    public AudioSource Rain;
    private bool FadeTo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            FadeTo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FadeTo = false;
        }
    }


    private void Update()
    {
        if (FadeTo)
        {
            if(Rain.volume > 0.3f)
            {
                Rain.volume -= 0.182f * Time.deltaTime;
            }

            if (Rain.GetComponent<AudioLowPassFilter>().cutoffFrequency > 3975)
            {
                Rain.GetComponent<AudioLowPassFilter>().cutoffFrequency -= 18025 * Time.deltaTime;
            }
        }
        else
        {
            if (Rain.volume < 0.482f)
            {
                Rain.volume += 0.182f * Time.deltaTime;
            }

            if (Rain.GetComponent<AudioLowPassFilter>().cutoffFrequency < 22000)
            {
                Rain.GetComponent<AudioLowPassFilter>().cutoffFrequency += 18025 * Time.deltaTime;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRain : MonoBehaviour
{
    public List<GameObject> Deactivate;

    public List<AudioSource> KitchenAudio = new List<AudioSource>();
    public List<AudioLowPassFilter> InsideAtmos = new List<AudioLowPassFilter>();

    private void OnTriggerEnter(Collider other)
    {
        foreach (AudioLowPassFilter filter in InsideAtmos)
        {
            filter.cutoffFrequency = 3975;
        }

        foreach (AudioSource audio in KitchenAudio)
        {
            audio.Play();
        }

        foreach (GameObject thing in Deactivate)
        {
            thing.SetActive(false);
        }

        Destroy(this);
    }
}

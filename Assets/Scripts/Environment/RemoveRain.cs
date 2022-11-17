using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRain : MonoBehaviour
{
    public GameObject rain;

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

        rain.SetActive(false);
        Destroy(this.gameObject);
    }
}

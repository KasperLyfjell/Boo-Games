using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorLock : MonoBehaviour
{
    public List<AudioSource> KitchenAudio = new List<AudioSource>();
    public AudioLowPassFilter InsideAtmos;

    public AudioSource WindGust;
    public Env_Door KitchenDoor;

    private void OnTriggerEnter(Collider other)
    {
        InsideAtmos.cutoffFrequency = 3975;
        foreach(AudioSource audio in KitchenAudio)
        {
            audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        WindGust.Play();
        KitchenDoor.ShutDoor();

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNeedingKey1 : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    public GameObject key;
    bool played;

    public AudioClip[] doorSounds;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void OpenDoor()
    {
        if (key == null && played == false)
        {
            played = true;
            anim.SetBool("Open", true);
            audioSource.clip = doorSounds[0];
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            audioSource.clip = doorSounds[1];
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}

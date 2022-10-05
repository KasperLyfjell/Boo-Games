using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [Tooltip("AudioSource where the audio is played from")]
    public AudioSource Source;

    [Tooltip ("Should I trigger only once?")]
    public bool TriggerOnce;
    private bool triggered;

    public bool TriggerOnEnter;
    public bool TriggerOnExit;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Player" && TriggerOnEnter)
        {
            TriggerCheck();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player" && TriggerOnExit)
        {
            TriggerCheck();
        }
    }

    private void TriggerCheck()
    {
        if (TriggerOnce)
        {
            if (!triggered)
            {
                PlaySound();
                triggered = true;
            }
        }
        else
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        Source.Play();
    }
}

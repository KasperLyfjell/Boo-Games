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

    public bool FadeOutAudio;
    public AudioSource AudioToFade;

    public bool FollowUpAudio;
    public AudioSource followAudio;
    public float followDelay;

    public bool FadeIn;
    public bool FadeOut;
    public float FadeTo = 1;//Volume % to fade to


    private bool fade;
    private float delay;



    private void Update()
    {
        if (fade)
        {
            if (!FadeIn)
            {
                AudioToFade.volume -= delay * Time.deltaTime;
                Source.volume += delay * Time.deltaTime;
            }
            else
            {
                if (FadeOut)
                {
                    AudioToFade.volume -= 0.4f * Time.deltaTime;
                }

                Source.volume += 0.4f * Time.deltaTime;

                if (Source.volume >= FadeTo)
                    fade = false;
            }

        }
    }

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

        if (FadeOutAudio)
            StartCoroutine(Fadeout(0.6f));

        if(FollowUpAudio)
            StartCoroutine(FollowUp(Source.clip.length + followDelay));

        if (FadeIn)
        {
            fade = true;
        }

    }

    IEnumerator Fadeout(float duration)
    {
        delay = duration;
        Source.volume = 0;
        Source.time = AudioToFade.time;
        fade = true;

        yield return new WaitForSeconds(duration);

        fade = false; 

    }

    IEnumerator FollowUp(float delay)
    {
        yield return new WaitForSeconds(delay);

        followAudio.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI SubtitleObj;
    public List<string> Subtitles = new List<string>();



    private void Update()
    {
        if (fade)
        {
            if (!FadeIn)
            {
                AudioToFade.volume -= Time.deltaTime / delay;
                Source.volume += 1.5f * Time.deltaTime / delay;
            }
            else
            {
                if (FadeOut)
                {
                    AudioToFade.volume -= 0.5f * Time.deltaTime;
                }

                Source.volume += 0.5f * Time.deltaTime;

                
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

        if (FadeOutAudio)
            StartCoroutine(Fadeout(2.5f));

        if(FollowUpAudio)
            StartCoroutine(FollowUp(Source.clip.length + followDelay));

        if (FadeIn)
        {
            fade = true;
        }

        Source.Play();

        if(SubtitleObj != null && Subtitles[0] != null)
            StartCoroutine(DisplaySubtitles(Subtitles[0], Source.clip.length));

    }

    IEnumerator Fadeout(float duration)
    {
        delay = duration;
        Source.volume = 0;

        //if(Source.clip.length <= AudioToFade.time)
            Source.time = AudioToFade.time;

        fade = true;

        yield return new WaitForSeconds(duration);

        fade = false;
    }

    IEnumerator FollowUp(float delay)
    {
        yield return new WaitForSeconds(delay);

        followAudio.Play();

        if (Subtitles[1] != null)
            StartCoroutine(DisplaySubtitles(Subtitles[1], followAudio.clip.length));
    }

    IEnumerator DisplaySubtitles(string text, float duration)
    {
        SubtitleObj.text = text;
        SubtitleObj.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration + 0.2f);

        SubtitleObj.gameObject.SetActive(false);
    }
}

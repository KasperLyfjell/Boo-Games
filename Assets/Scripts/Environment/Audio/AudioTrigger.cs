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

    public FlashbackTrigger TransitionFlashback;
    public float flashbackTransitionDelay;

    public AudioTrigger AsyncAudio;
    public float asyncaudioDelay;

    public AudioTrigger PlayNewAfter;
    public float newAfterDelay;

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

    public void PlaySound()
    {

        if (FadeOutAudio)
            StartCoroutine(Fadeout(2.5f));

        if (FollowUpAudio)
        {
            StartCoroutine(FollowUp(Source.clip.length + followDelay));

            if(PlayNewAfter != null)
            {
                StartCoroutine(AsyncPlayer(Source.clip.length + followDelay + followAudio.clip.length + newAfterDelay));
            }
        }
        else if(PlayNewAfter != null)
        {
            StartCoroutine(AsyncPlayer(Source.clip.length + newAfterDelay));
        }

        if (FadeIn)
        {
            fade = true;
        }

        Source.Play();

        if(SubtitleObj != null && Subtitles[0] != null)
            StartCoroutine(DisplaySubtitles(Subtitles[0], Source.clip.length, true));

        if (AsyncAudio != null)
            StartCoroutine(AsyncPlayer(asyncaudioDelay));
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

        if (SubtitleObj != null && Subtitles[0] != null)
        {
            if (TransitionFlashback != null)
            {
                StartCoroutine(DisplaySubtitles(Subtitles[1], followAudio.clip.length, false));
            }
            else
                StartCoroutine(DisplaySubtitles(Subtitles[1], followAudio.clip.length, true));
        }

        if(TransitionFlashback != null)
        {
            yield return new WaitForSeconds(followAudio.clip.length + flashbackTransitionDelay);

            TransitionFlashback.StartFlashback();
        }
    }

    IEnumerator DisplaySubtitles(string text, float duration, bool disableSubtitles)
    {
        SubtitleObj.text = text;
        SubtitleObj.gameObject.SetActive(true);

        float delay = 0.2f;
        if (TransitionFlashback != null && !FollowUpAudio)
        {
            delay = flashbackTransitionDelay;
        }

        yield return new WaitForSeconds(duration + delay);

        if(disableSubtitles)
            SubtitleObj.gameObject.SetActive(false);

        if (TransitionFlashback != null && !FollowUpAudio)
        {
            TransitionFlashback.StartFlashback();
        }
    }

    IEnumerator AsyncPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (AsyncAudio != null)
            AsyncAudio.PlaySound();
        else if (PlayNewAfter != null)
            PlayNewAfter.PlaySound();

    }
}

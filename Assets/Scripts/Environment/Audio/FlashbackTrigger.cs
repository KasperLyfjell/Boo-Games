using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;
using UnityEngine.Audio;
using UnityEngine.VFX;

public class FlashbackTrigger : MonoBehaviour
{
    private int playedAudios;
    private AudioSource AU;
    private bool fadeTo;

    public bool CanTrigger;

    public AudioTrigger TriggerDialogue;

    [Header("Distortion VFX")]
    public GameObject ScreenDistortion;
    public SpriteRenderer Fade;
    public TextMeshProUGUI SubtitleObj;

    /*
    //This can be a way to easily switch between audio effects
    public AudioMixerGroup mixG;
    public AudioMixer mix;
    */

    [Header("Subtitle Details")]
    public List<string> Subtitles = new List<string>();
    [Tooltip("The total number of items in this list MUST be the same as the amount of items in 'Subtitle Details'")]
    public List<float> subDelay = new List<float>();//Have this the same number as the total number of subtitles

    private SUPERCharacterAIO player;

    [SerializeField] List<AudioMixer> mixers;
    private float cutoffValue;
    private bool isEnding;

    private void Start()
    {
        AU = GetComponent<AudioSource>();
        cutoffValue = 22000;
    }

    private void Update()
    {
        if (AU.isPlaying)
        {
            if (fadeTo)
            {
                if(Fade.color.a < 1)
                    Fade.color += new Color(0, 0, 0, 1.5f * Time.deltaTime);

                if(cutoffValue > 6000 && !isEnding)
                {
                    cutoffValue -= 9000 * Time.deltaTime;

                    foreach (AudioMixer mixer in mixers)
                    {
                        mixer.SetFloat("CutoffFreq", cutoffValue);
                    }
                }
            }
            else if (!fadeTo)
            {
                if(Fade.color.a > 0)
                    Fade.color -= new Color(0, 0, 0, 1.5f * Time.deltaTime);

                if (cutoffValue < 22000 && isEnding)
                {
                    cutoffValue += 10000 * Time.deltaTime;

                    foreach (AudioMixer mixer in mixers)
                    {
                        mixer.SetFloat("CutoffFreq", cutoffValue);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject.GetComponent<SUPERCharacterAIO>();

            if(CanTrigger)
                StartFlashback();
        }
    }

    public void StartFlashback()
    {
        StartCoroutine(InitiateFlashback());
        CanTrigger = false;
    }

    IEnumerator InitiateFlashback()
    {
        AU.Play();
        Fade.color = new Color(0, 0, 0, 0);
        fadeTo = true;

        yield return new WaitForSeconds(2);

        ScreenDistortion.SetActive(true);
        player.BeginFlashback();

        Fade.color = new Color(0, 0, 0, 1);
        fadeTo = false;
        StartCoroutine(PlayingAudio());
        StartCoroutine(PlayFlashback());
    }

    IEnumerator PlayFlashback()
    {
        SubtitleObj.text = Subtitles[playedAudios];
        SubtitleObj.gameObject.SetActive(true);

        yield return new WaitForSeconds(subDelay[playedAudios]);

        SubtitleObj.gameObject.SetActive(false);
        playedAudios++;

        if (playedAudios != Subtitles.Count)
            StartCoroutine(PlayFlashback());
    }

    IEnumerator PlayingAudio()
    {
        yield return new WaitForSeconds(AU.clip.length - 6);

        flashbackending(2);
    }

    private void flashbackending(float time)
    {
        StartCoroutine(EndFlashback(time));
    }

    IEnumerator EndFlashback(float endDelay)
    {
        isEnding = true;
        fadeTo = true;

        yield return new WaitForSeconds(endDelay);

        ScreenDistortion.SetActive(false);
        player.EndFlashback();

        if (TriggerDialogue != null)
        {
            TriggerDialogue.PlaySound();
        }

        fadeTo = false;
    }



    public void Activate()
    {
        CanTrigger = true;
    }
}

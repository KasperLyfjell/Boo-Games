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

    private void Start()
    {
        AU = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (AU.isPlaying)
        {
            if (fadeTo && Fade.color.a < 1)
            {
                Debug.Log("Im fading in");
                Fade.color += new Color(0, 0, 0, 1.5f * Time.deltaTime);
            }
            else if (!fadeTo && Fade.color.a > 0)
            {
                Debug.Log("Im fading out");
                Fade.color -= new Color(0, 0, 0, 1.5f * Time.deltaTime);
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
        fadeTo = true;

        yield return new WaitForSeconds(2);

        ScreenDistortion.SetActive(true);
        player.BeginFlashback();

        fadeTo = false;
        StartCoroutine(PlayFlashback());
    }

    IEnumerator PlayFlashback()
    {
        SubtitleObj.text = Subtitles[playedAudios];
        SubtitleObj.gameObject.SetActive(true);

        yield return new WaitForSeconds(subDelay[playedAudios]);

        SubtitleObj.gameObject.SetActive(false);
        playedAudios++;

        if (playedAudios == Subtitles.Count)
            StartCoroutine(EndFlashback());
        else
            StartCoroutine(PlayFlashback());
    }

    IEnumerator EndFlashback()
    {
        fadeTo = true;

        yield return new WaitForSeconds(1);

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

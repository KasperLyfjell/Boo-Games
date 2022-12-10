using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;
using UnityEngine.Audio;

public class FlashbackTrigger : MonoBehaviour
{
    private int playedAudios;
    private AudioSource AU;

    public bool CanTrigger;

    public AudioTrigger TriggerDialogue;

    [Header("Distortion VFX")]
    public GameObject ScreenDistortion;
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

        yield return new WaitForSeconds(1f);

        player.BeginFlashback();
        ScreenDistortion.SetActive(true);
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
            EndFlashback();
        else
            StartCoroutine(PlayFlashback());
    }

    private void EndFlashback()        //stop the wibbly wobbly effect and go transition to normal
    {
        ScreenDistortion.SetActive(false);
        player.EndFlashback();

        if (TriggerDialogue != null)
        {
            TriggerDialogue.PlaySound();
        }
    }


    public void Activate()
    {
        CanTrigger = true;
    }
}

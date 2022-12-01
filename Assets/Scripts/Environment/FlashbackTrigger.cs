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
        if(other.gameObject.name == "Player" && CanTrigger)
        {
            //trigger the wibbly wobbly effect and transition to flashback
            //propably could be done with another coroutine to transition into flashback mode, which then it starts the sound
            player = other.gameObject.GetComponent<SUPERCharacterAIO>();
            StartCoroutine(InitiateFlashback());
            CanTrigger = false;
        }
    }

    IEnumerator InitiateFlashback()
    {


        yield return new WaitForSeconds(2f);

        player.BeginFlashback();
        ScreenDistortion.SetActive(true);
        StartCoroutine(PlayFlashback());
    }

    IEnumerator PlayFlashback()
    {
        //Propably add some initiation first to fade into flashback
        AU.Play();
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
    }
}

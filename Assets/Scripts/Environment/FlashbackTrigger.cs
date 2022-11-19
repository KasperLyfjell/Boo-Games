using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;

public class FlashbackTrigger : MonoBehaviour
{
    private int playedAudios;
    private AudioSource AU;

    public bool CanTrigger;

    public GameObject ScreenDistortion;
    public TextMeshProUGUI SubtitleObj;

    public List<AudioClip> Clips = new List<AudioClip>();
    public List<string> Subtitles = new List<string>();

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
            player.BeginFlashback();
            StartCoroutine(PlayFlashback());
            ScreenDistortion.SetActive(true);
            CanTrigger = false;
        }
    }

    IEnumerator PlayFlashback()
    {
        AU.clip = Clips[playedAudios];
        AU.Play();
        SubtitleObj.text = Subtitles[playedAudios];
        SubtitleObj.gameObject.SetActive(true);

        yield return new WaitForSeconds(AU.clip.length + 0.1f);

        SubtitleObj.gameObject.SetActive(false);
        playedAudios++;

        if (playedAudios == Clips.Count)
            EndFlashback();
        else
            StartCoroutine(PlayFlashback());//if that even works??
    }

    private void EndFlashback()
    {
        //stop the wibbly wobbly effect and go transition to normal
        ScreenDistortion.SetActive(false);
        player.EndFlashback();
    }
}

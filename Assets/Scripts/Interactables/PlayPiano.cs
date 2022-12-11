using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPiano : MonoBehaviour
{
    AudioSource au;
    public AudioClip pianoSFX;
    bool playing;

    public static int hits;
    public AudioSource Stab;

    // Start is called before the first frame update
    void Start()
    {
        hits = Random.Range(20, 41);
        au = GetComponent<AudioSource>();
    }

    /*

    public void Piano()
    {
        if(playing == false)
        {
            playing = true;
            Invoke("DonePlaying", 7f);
            au.PlayOneShot(pianoSFX);
        }
    }

    void DonePlaying()
    {
        playing = false;
    }
    */

    public void PlayKey()
    {
        if(Stab.isPlaying == false)
        {
            hits--;

            if (hits == 0)
            {
                Stab.Play();
                hits = Random.Range(20, 41);
            }
            else
                au.Play();
        }
    }
}

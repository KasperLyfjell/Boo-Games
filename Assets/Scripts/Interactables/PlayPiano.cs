using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPiano : MonoBehaviour
{
    AudioSource au;
    public AudioClip pianoSFX;
    bool playing;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

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
}

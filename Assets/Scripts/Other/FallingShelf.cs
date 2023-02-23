using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShelf : MonoBehaviour
{
    public LanternWheelController lanternWheel;
    public GameObject standingShelf;
    public GameObject lyingShelf;

    AudioSource au;
    public AudioClip audioClip;

    bool played;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        lyingShelf.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(played == false)
        {
            if(lanternWheel.redCollected == true)
            {
                played = true;
                standingShelf.SetActive(false);
                lyingShelf.SetActive(true);
                au.PlayOneShot(audioClip);
            }
        }
    }
}

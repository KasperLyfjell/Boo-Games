using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    Animator anim;
    bool open;
    AudioSource au;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }

    public void InteractDrawer()
    {
        if (open == false)
        {
            open = true;
            anim.SetBool("Interact", true);
            au.PlayOneShot(audioClip);
        }
        else
        {
            open = false;
            anim.SetBool("Interact", false);
            au.PlayOneShot(audioClip);
        }
    }

}

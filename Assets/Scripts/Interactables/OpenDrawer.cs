using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    Animator anim;
    bool open;
    bool inAnimation;
    AudioSource au;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        au = GetComponent<AudioSource>();
    }

    public void InteractDrawer()
    {
        if(inAnimation == false)
        {
            if (open == false)
            {
                open = true;
                inAnimation = true;
                Invoke("FinishedAnimation", 1f);
                anim.SetBool("Open", true);
                au.PlayOneShot(audioClip);
            }
            else
            {
                open = false;
                inAnimation = true;
                Invoke("FinishedAnimation", 1f);
                anim.SetBool("Open", false);
                au.PlayOneShot(audioClip);
            }
        }
    }

    void FinishedAnimation()
    {
        inAnimation = false;
    }

}

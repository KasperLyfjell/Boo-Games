using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    Animator anim;
    bool open;
    bool inAnimation;
    AudioSource au;

    public bool isLocked;
    public AudioClip openDoor;
    public AudioClip doorLocked;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        au = GetComponent<AudioSource>();
    }

    public void InteractDrawer()
    {
        if(inAnimation == false && isLocked == false)
        {
            if (open == false)
            {
                open = true;
                inAnimation = true;
                Invoke("FinishedAnimation", 1f);
                anim.SetBool("Open", true);
                PlayAudio(openDoor);
            }
            else
            {
                open = false;
                inAnimation = true;
                Invoke("FinishedAnimation", 1f);
                anim.SetBool("Open", false);
                PlayAudio(openDoor);
            }
        }
        else
        {
            PlayAudio(doorLocked);
        }
    }

    void FinishedAnimation()
    {
        inAnimation = false;
    }

    private void PlayAudio(AudioClip clip)
    {
        au.clip = clip;
        au.Play();
    }

}

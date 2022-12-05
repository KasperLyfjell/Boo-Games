using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCabinet : MonoBehaviour
{
    Animator anim;
    bool open;
    bool inAnimation;
    AudioSource au;

    public bool isLocked;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    public AudioClip doorLocked;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }

    public void InteractCabinet()
    {
        if(inAnimation == false && isLocked == false)
        {
            if (open == false)
            {
                open = true;
                inAnimation = true;
                Invoke("FinishedAnimation", 1f);
                anim.SetBool("Open", true);
                au.PlayOneShot(openDoor);
            }
            else
            {
                open = false;
                inAnimation = true;
                Invoke("FinishedAnimation", 1f);
                anim.SetBool("Open", false);
                au.PlayOneShot(closeDoor);
            }
        }
        else
        {
            au.PlayOneShot(doorLocked);
        }
    }

    void FinishedAnimation()
    {
        inAnimation = false;
    }

}

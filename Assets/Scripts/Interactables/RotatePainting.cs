using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePainting : MonoBehaviour
{
    Animator anim;
    public bool correct;
    bool playing;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Rotate()
    {
        if (playing == false)
        {
            if (correct == false)
            {
                correct = true;
                playing = true;
                Invoke("DonePlaying", 1f);
                anim.SetBool("Correct", true);
            }
            else
            {
                correct = false;
                playing = true;
                Invoke("DonePlaying", 1f);
                anim.SetBool("Correct", false);
            }
        }
    }

    void DonePlaying()
    {
        playing = false;
    }
}

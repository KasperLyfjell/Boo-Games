using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallSolutionCheck : MonoBehaviour
{
    public AudioClip openSound;

    public BallButton[] ballButtons;
    public BallButton[] fakeBallButtons;
    bool played;

    Animator anim;
    AudioSource au;

    public Interactable boxBlocker;

    private void Start()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (played == false)
        {
            if (fakeBallButtons[0].ballActive == false && fakeBallButtons[1].ballActive == false && 
                fakeBallButtons[2].ballActive == false && fakeBallButtons[3].ballActive == false && 
                fakeBallButtons[4].ballActive == false && fakeBallButtons[5].ballActive == false && 
                fakeBallButtons[6].ballActive == false && fakeBallButtons[7].ballActive == false && 
                fakeBallButtons[8].ballActive == false && fakeBallButtons[9].ballActive == false && 
                fakeBallButtons[10].ballActive == false && fakeBallButtons[11].ballActive == false)
            {
                if (ballButtons[0].ballActive == true && ballButtons[1].ballActive == true && ballButtons[2].ballActive == true)
                {
                    //Put whatever is supposed to happen when puzzle is complete here
                    played = true;
                    Open();
                }
            }
        }
    }

    void Open()
    {
        boxBlocker.enabled = false;
        anim.SetBool("Open", true);
        au.PlayOneShot(openSound);
    }
}

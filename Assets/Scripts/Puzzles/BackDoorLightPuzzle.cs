using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorLightPuzzle : MonoBehaviour
{
    public Light needsBlue;
    public Light needsYellow;
    LanternWheelController lwController;
    Animator gateDoorAnim;
    bool played = false;

    AudioSource au;
    public AudioClip audioClip;

    private void Start()
    {
        lwController = GameObject.FindWithTag("LanternWheel").GetComponent<LanternWheelController>();
        gateDoorAnim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (needsBlue.color == Color.blue && needsYellow.color == lwController.defaultColor && played == false)
        {
            //Put whatever is supposed to happen when puzzle is complete here
            played = true;
            Invoke("OpenDoor", 1f);
        }
    }

    void OpenDoor()
    {
        //-Bartosz-: Swapped out the functionality to unlock the door script instead of triggering an animation
        GetComponent<Env_Door>().Unlock();
        au.PlayOneShot(audioClip);
        //gateDoorAnim.SetBool("Open", true);
    }
}

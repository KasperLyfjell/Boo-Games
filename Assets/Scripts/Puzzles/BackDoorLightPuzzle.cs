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

    public GameObject MansionInsides;

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
        MansionInsides.SetActive(true);//Attempts to fix the performance issue by spawning the inside of the mansion only after unlocking the kitchen door
        GetComponent<Env_Door>().MoveOpenDoor(30);
        //gateDoorAnim.SetBool("Open", true);
    }
}

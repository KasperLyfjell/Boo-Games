using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLightPuzzle : MonoBehaviour
{
    public Light rightLampLight;
    LanternWheelController lwController;
    Animator gateDoorAnim;
    bool played = false;

    private void Start()
    {
        lwController = GameObject.FindWithTag("LanternWheel").GetComponent<LanternWheelController>();
        gateDoorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightLampLight.color == lwController.defaultColor && played == false)
        {
            //Put whatever is supposed to happen when puzzle is complete here
            played = true;
            Invoke("OpenDoor", 2f);
        }
    }

    void OpenDoor()
    {
        gateDoorAnim.SetBool("Open", true);
        //-Bartosz-: Added the play sound function as well as an audiosource on the game with the correct SFX
        GetComponent<AudioSource>().Play();
    }
}

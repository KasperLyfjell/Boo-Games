using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class EndingTrigger : MonoBehaviour
{
    private bool IsInside;
    private bool triggered;

    public LanternWheelController LanternWheel;
    public Light lanternlight;

    public Env_Door BedroomDoor1;
    public Env_Door BedroomDoor2;

    public AudioSource bang;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !triggered)
        {
            IsInside = true;
        }
    }

    private void Update()
    {
        if(IsInside && lanternlight.color == Color.red && !triggered)
        {
            StartEnding();
        }
    }

    private void StartEnding()
    {
        triggered = true;
        BedroomDoor1.ShutDoor();
        BedroomDoor2.ShutDoor();

        bang.Play();
        Invoke("delaydFlashback", 2);
    }

    void delaydFlashback()
    {
        GetComponent<FlashbackTrigger>().StartFlashback();
    }
}

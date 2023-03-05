using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class EndingTrigger : MonoBehaviour
{
    private bool IsInside;
    private bool triggered;
    private bool doorsClosed;

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

            if (!doorsClosed)
                close();
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

        LanternWheel.CanInteract = false;
        Invoke("delaydFlashback", 1);
    }

    void delaydFlashback()
    {
        GetComponent<FlashbackTrigger>().StartFlashback();
    }

    void close()
    {
        BedroomDoor1.ShutDoor();
        BedroomDoor2.ShutDoor();
        bang.Play();
        doorsClosed = true;
    }
}

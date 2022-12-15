using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class EndingTrigger : MonoBehaviour
{
    private bool IsInside;
    private bool triggered;

    private SUPERCharacterAIO player;
    public LanternWheelController LanternWheel;
    public Light lanternlight;

    public Env_Door BedroomDoor1;
    public Env_Door BedroomDoor2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            player = other.gameObject.GetComponent<SUPERCharacterAIO>();

            if (!triggered)
            {
                if(lanternlight.color == Color.red)
                {
                    StartEnding();
                }
                else
                    IsInside = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            IsInside = false;
        }
    }

    private void Update()
    {
        if(!triggered && IsInside /*&& color red equipped*/)
        {
            StartEnding();
        }
    }

    private void StartEnding()
    {
        triggered = true;
        BedroomDoor1.ShutDoor();
        BedroomDoor2.ShutDoor();
        GetComponent<FlashbackTrigger>().StartFlashback();
    }
}

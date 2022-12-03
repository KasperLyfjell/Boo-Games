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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            player = other.gameObject.GetComponent<SUPERCharacterAIO>();
            IsInside = true;

            /*
            if(color red is equipped && !triggered)
            {
                StartEnding();
            }
            */
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
        gameObject.GetComponent<FlashbackTrigger>().StartFlashback();
    }
}

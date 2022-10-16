using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class Playerspeedup : MonoBehaviour
{
    public float SpeedIncrease;
    private bool trigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !trigger)
        {
            other.gameObject.GetComponent<SUPERCharacterAIO>().SpeedUp(SpeedIncrease);
            trigger = true;
        }
    }
}

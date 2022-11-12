using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : MonoBehaviour
{
    public SUPERCharacter.SUPERCharacterAIO super;

    private void OnTriggerEnter(Collider other)
    {
        super.sprintingSpeed = 350;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class ChaseTransition : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            other.GetComponent<SUPERCharacterAIO>().BeginChase(75, 300);

            //Disable lantern wheel
        }
    }
}

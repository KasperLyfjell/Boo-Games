using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseCrestPuzzle : MonoBehaviour
{
    public Light lanternLight;
    public LanternWheelController lanternWheel;
    public GameObject horse;

    private void Update()
    {
        if(!lanternWheel.lighterEquipped && lanternLight.color == Color.green)
        {
            horse.SetActive(true);
        }
        else
        {
            horse.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPrintPuzzle : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(door);
        }
    }
}

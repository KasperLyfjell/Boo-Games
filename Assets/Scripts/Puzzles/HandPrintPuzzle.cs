using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPrintPuzzle : MonoBehaviour
{
    public GameObject door;
    bool isClose;

    private void Update()
    {
        if (isClose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(door);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isClose = false;
    }
}

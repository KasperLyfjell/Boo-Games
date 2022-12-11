using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorLock : MonoBehaviour
{
    public AudioSource WindGust;
    public Env_Door KitchenDoor;
    private bool canttrigger;

    private void OnTriggerExit(Collider other)
    {
        if (!canttrigger)
        {
            WindGust.Play();
            KitchenDoor.ShutDoor();
            canttrigger = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorLock : MonoBehaviour
{
    public AudioSource WindGust;
    public Env_Door KitchenDoor;

    private void OnTriggerEnter(Collider other)
    {
        WindGust.Play();
        KitchenDoor.ShutDoor();

        Destroy(gameObject);
    }

}

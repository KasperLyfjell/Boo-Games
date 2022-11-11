using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject DoorToUnlock;

    public bool LastKey;//This is only temporary thing 

    public void PickUpKey()
    {
        if (LastKey)
            DoorToUnlock.GetComponent<temp_Door>().HasKey = true;
        else
            DoorToUnlock.GetComponent<Env_Door>().Unlock();

        Destroy(this.gameObject);
    }
}

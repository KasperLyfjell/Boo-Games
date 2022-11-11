using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public Env_Door DoorToUnlock;

    public void PickUpKey()
    {
        DoorToUnlock.Unlock();
        Destroy(this.gameObject);
    }
}

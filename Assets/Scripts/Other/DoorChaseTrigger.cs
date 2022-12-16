using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChaseTrigger : MonoBehaviour
{
    public Env_Door door;
    public bool Close;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            if (Close)
                door.ShutDoor();
            else
                door.MoveOpenDoor(90);

            Destroy(gameObject);
        }
    }
}

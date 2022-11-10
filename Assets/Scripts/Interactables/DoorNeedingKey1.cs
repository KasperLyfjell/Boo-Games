using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNeedingKey1 : MonoBehaviour
{
    Animator anim;
    public GameObject key;
    bool played;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (key == null && played == false)
        {
            played = true;
            anim.SetBool("Open", true);
        }
    }
}

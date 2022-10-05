using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseLantern : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("Raise", true);
        }
        else
        {
            anim.SetBool("Raise", false);
        }
    }
}

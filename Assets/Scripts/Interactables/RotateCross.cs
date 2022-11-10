using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCross : MonoBehaviour
{
    Animator anim;
    bool correct;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Rotate()
    {
        if(correct == false)
        {
            correct = true;
            anim.SetBool("Correct", true);
        }
        else
        {
            correct = false;
            anim.SetBool("Correct", false);
        }
    }
}

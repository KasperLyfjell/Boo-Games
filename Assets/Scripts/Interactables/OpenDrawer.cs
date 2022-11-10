using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    Animator anim;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InteractDrawer()
    {
        if (open == false)
        {
            open = true;
            anim.SetBool("Interact", true);
        }
        else
        {
            open = false;
            anim.SetBool("Interact", false);
        }
    }

}

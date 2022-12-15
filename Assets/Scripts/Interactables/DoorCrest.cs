using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCrest : MonoBehaviour
{
    Animator crestAnim;
    public Animator smallCylAnim;
    public Animator bigCylAnim;
    public GameObject crest;
    public GameObject crestIcon;
    public GameObject crestPlacement;
    public GameObject textPopup;
    MeshRenderer text;

    bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        text = textPopup.GetComponent<MeshRenderer>();
        text.enabled = false;
        crestAnim = crestPlacement.GetComponent<Animator>();
        crestPlacement.SetActive(false);
    }

    public void InsertCrest()
    {
        if(played == false)
        {
            if(crest != null)
            {
                played = true;
                text.enabled = true;
                Invoke("TextOff", 2f);
            }
            else
            {
                played = true;
                crestIcon.SetActive(false);
                crestPlacement.SetActive(true);
                crestAnim.SetBool("Place", true);
                Invoke("RotateCylinders", 1f);
            }
        }
    }

    void RotateCylinders()
    {
        smallCylAnim.SetBool("Rotate", true);
        bigCylAnim.SetBool("Rotate", true);
        Invoke("MoveSmallCylinderIn", 1f);
    }

    void MoveSmallCylinderIn()
    {
        smallCylAnim.SetBool("MoveIn", true);
        Invoke("UnlockDoor", 1f);
    }

    void TextOff()
    {
        text.enabled = false;
        played = false;
    }

    void UnlockDoor()
    {
        //HER BARTOSZ
    }
}

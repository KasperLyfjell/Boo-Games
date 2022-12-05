using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCrest : MonoBehaviour
{
    public Animator crestAnim;
    public Animator smallCylAnim;
    public Animator bigCylAnim;
    public GameObject crest;
    public GameObject crestPlacement;

    // Start is called before the first frame update
    void Start()
    {
        crestPlacement.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertCrest()
    {
        if(crest != null)
        {
            Debug.Log("YAS");
        }
        else
        {
            Debug.Log("NAIS");
            crestPlacement.SetActive(true);
            crestAnim.SetBool("Place", true);
            Invoke("RotateCylinders", 1f);
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
    }
}

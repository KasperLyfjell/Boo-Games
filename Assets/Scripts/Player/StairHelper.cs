using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairHelper : MonoBehaviour
{
    public GameObject stairRayPosbot;
    public GameObject stairRayPostop;



    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
            specialStairMovement();
    }


    void specialStairMovement()
    {
        RaycastHit hitLow;
        if (Physics.Raycast(stairRayPosbot.transform.position, transform.TransformDirection(Vector3.forward), out hitLow, 1))
        {
            if(hitLow.collider.tag == "Stair")
            {
                GetComponent<Rigidbody>().position += new Vector3(0f, 0.15f, 0);
            }
        }
    }
}

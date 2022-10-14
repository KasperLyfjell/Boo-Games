using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneStart : MonoBehaviour
{
    LanternWheelController wheelCon;

    // Start is called before the first frame update
    void Start()
    {
        wheelCon = GameObject.Find("LanternWheel").GetComponent<LanternWheelController>();

        wheelCon.CanInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

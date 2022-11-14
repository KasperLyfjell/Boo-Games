using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallButton : MonoBehaviour
{
    Light ballLight;
    public bool ballActive;

    // Start is called before the first frame update
    void Start()
    {
        ballLight = GetComponent<Light>();
        ballLight.enabled = false;
        ballActive = false;
    }

    public void PressButton()
    {
        if(ballActive == false)
        {
            ballActive = true;
            ballLight.enabled = true;
        }
        else
        {
            ballActive = false;
            ballLight.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLightsPuzzle : MonoBehaviour
{
    public LanternWheelController lwController;
    public Light[] candleLightColors;
    Animator anim;
    bool played;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(played == false)
        {
            if (candleLightColors[0].color == lwController.defaultColor && candleLightColors[1].color == Color.green && candleLightColors[2].color == Color.blue)
            {
                //Put whatever is supposed to happen when puzzle is complete here
                played = true;
                Open();
            }
        }
    }

    void Open()
    {
        anim.SetBool("Open", true);
    }
}

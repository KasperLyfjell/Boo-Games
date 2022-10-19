using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLightsPuzzle : MonoBehaviour
{
    public Light[] candleLightColors;
    LanternWheelController lwController;


    private void Start()
    {
        lwController = GameObject.Find("LanternWheel").GetComponent<LanternWheelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (candleLightColors[0].color == Color.green && candleLightColors[1].color == Color.blue && candleLightColors[2].color == lwController.defaultColor)
        {
            //Put whatever is supposed to happen when puzzle is complete here
            Destroy(gameObject);
        }
    }
}

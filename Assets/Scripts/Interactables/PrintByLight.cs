using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PrintByLight : MonoBehaviour
{
    [SerializeField] LanternColor Lcolor = new LanternColor();
    private Color color;

    public Light lanternLightColor;
    bool close = false;
    DecalProjector dp;

    MeshRenderer Mesh;

    public LanternWheelController wheelController;

    public Light candleLight;

    enum LanternColor
    {
        Blue,
        Green,
        Red
    }

    private void Start()
    {
        //GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //lanternLightColor = manager.LanternLight;

        dp = GetComponentInChildren<DecalProjector>();
        dp.fadeFactor = 0f;

        switch (Lcolor)
        {
            case LanternColor.Blue:
                color = Color.blue;
                break;
            case LanternColor.Green:
                color = Color.green;
                break;
            case LanternColor.Red:
                color = Color.red;
                break;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelController.lighterEquipped == false && close == true && lanternLightColor.color == color)
        {
            dp.fadeFactor = 1f;
        }
        else if (candleLight.color == color)
        {
            dp.fadeFactor = 1f;
        }
        else
        {
            dp.fadeFactor = 0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        close = true;
    }

    private void OnTriggerExit(Collider other)
    {
        close = false;
    }
}

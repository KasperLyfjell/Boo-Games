using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField]
    Type reactToColor = new Type();
    private Color color;

    bool close = false;
    DecalProjector dp;
    Light lanternLightColor;
    LanternWheelController wheelCon;

    public Material normalMaterial;
    public Material creepyMaterial;
    

    // Start is called before the first frame update
    void Start()
    {
        dp = GetComponent<DecalProjector>();
        lanternLightColor = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        wheelCon = GameObject.Find("LanternWheel").GetComponent<LanternWheelController>();

        switch (reactToColor)
        {
            case Type.YellowLight:
                color = Color.yellow;
                break;

            case Type.BlueLight:
                color = Color.blue;
                break;

            case Type.GreenLight:
                color = Color.green;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelCon.lighterEquipped == false && close == true && lanternLightColor.color == color)
        {
            dp.material = creepyMaterial;
        }
        else
        {
            dp.material = normalMaterial;
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

    enum Type
    {
        YellowLight,
        BlueLight,
        GreenLight
    };
}

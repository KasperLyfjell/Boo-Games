using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField]
    Type reactToColor = new Type();
    private Color color;

    //bool close = false;
    DecalProjector dp;
    public Light lanternLight;
    public LanternWheelController lanternWheel;

    public Material normalMaterial;
    public Material creepyMaterial;
    

    // Start is called before the first frame update
    void Start()
    {
        dp = GetComponent<DecalProjector>();
        //lanternLightColor = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        //wheelCon = GameObject.Find("LanternWheel").GetComponent<LanternWheelController>();

        switch (reactToColor)
        {
            case Type.YellowLight:
                color = lanternWheel.defaultColor;
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
        if (lanternWheel.lighterEquipped == false && lanternLight.color == color)
        {
            dp.material = creepyMaterial;
        }
        else
        {
            dp.material = normalMaterial;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        close = true;
    }

    private void OnTriggerExit(Collider other)
    {
        close = false;
    }*/

    enum Type
    {
        YellowLight,
        BlueLight,
        GreenLight
    };
}

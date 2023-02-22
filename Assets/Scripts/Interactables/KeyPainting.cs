using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class KeyPainting : MonoBehaviour
{
    [SerializeField]
    Type reactToColor = new Type();
    private Color color;

    DecalProjector dp;
    public Light lanternLight;
    public LanternWheelController lanternWheel;

    public Material normalMaterial;
    public Material afterMaterial;
    public GameObject crest;


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
        if(crest != null)
        {
            if (lanternWheel.lighterEquipped == false && lanternLight.color == color && lanternWheel.redCollected == true)
            {   
                dp.material = afterMaterial;
                crest.SetActive(true);
            }
            else
            {
                dp.material = normalMaterial;
                crest.SetActive(false);
            }
        }
        else
        {
            dp.material = afterMaterial;
        }
    }

    enum Type
    {
        YellowLight,
        BlueLight,
        GreenLight
    };
}

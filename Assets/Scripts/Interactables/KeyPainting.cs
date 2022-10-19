using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class KeyPainting : MonoBehaviour
{
    [SerializeField]
    Type reactToColor = new Type();
    private Color color;

    bool close = false;
    DecalProjector dp;
    public Light lanternLight;
    public LanternWheelController lanternWheel;

    public Material normalMaterial;
    public Material afterMaterial;
    public GameObject key;
    public DoorNeedingKey door;


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
        if(door.haveKey == false)
        {
            if (lanternWheel.lighterEquipped == false && close == true && lanternLight.color == color)
            {   
                dp.material = afterMaterial;
                key.SetActive(true);
            }
            else
            {
                dp.material = normalMaterial;
                key.SetActive(false);
            }
        }
        else
        {
            dp.material = afterMaterial;
        }
    }

    public void PickUpKey()
    {
        door.haveKey = true;
        Destroy(key);
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

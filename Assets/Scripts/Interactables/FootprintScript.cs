using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class FootprintScript : MonoBehaviour
{
    [SerializeField] ObjectType Type = new ObjectType();
    [SerializeField] LanternColor Lcolor = new LanternColor();
    private Color color;

    Light lanternLightColor;
    bool close = false;
    DecalProjector dp;

    MeshRenderer Mesh;



    enum ObjectType
    {
        Decal,//Decals, such as footprints, blood or other non-interactable events
        Keys//Keys and possibly other interactable objects which unlock new areas
    }

    enum LanternColor
    {
        Blue,
        Green,
        Red
    }

    private void Start()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lanternLightColor = manager.LanternLight;

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

        switch (Type)
        {
            case ObjectType.Decal:
                dp = GetComponentInChildren<DecalProjector>();
                dp.fadeFactor = 0f;
                break;
            case ObjectType.Keys:
                Mesh = GetComponent<MeshRenderer>();
                Mesh.enabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (close == true && lanternLightColor.color == color)
        {
            switch (Type)
            {
                case ObjectType.Decal:
                    dp.fadeFactor = 1f;
                    break;
                case ObjectType.Keys:
                    Mesh.enabled = true;
                    break;
            }
        }
        else
        {
            switch (Type)
            {
                case ObjectType.Decal:
                    dp.fadeFactor = 0f;
                    break;
                case ObjectType.Keys:
                    Mesh.enabled = false;
                    break;
            }
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

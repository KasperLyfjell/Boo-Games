using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLight : MonoBehaviour
{
    [SerializeField]
    Type lightType = new Type();

    Animator lanternFlameColor;
    Light lanternLightColor;

    Light colorSelf;
    Animator flameSelf;
    ParticleSystem poofEffect;

    public RuntimeAnimatorController[] lightFlameColors;
    LanternWheelController lwController;

    bool playedBlue, playedGreen;

    // Start is called before the first frame update
    void Start()
    {
        lanternFlameColor = GameObject.Find("PlayerLanternFlame").GetComponent<Animator>();
        lanternLightColor = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        lwController = GameObject.Find("LanternWheel").GetComponent<LanternWheelController>();

        colorSelf = GetComponentInChildren<Light>();
        flameSelf = GetComponentInChildren<Animator>();
        poofEffect = GetComponentInChildren<ParticleSystem>();

        switch (lightType)
        {
            case Type.YellowLight:
                colorSelf.color = Color.yellow;
                flameSelf.runtimeAnimatorController = lightFlameColors[0];
                break;

            case Type.BlueLight:
                colorSelf.color = Color.blue;
                flameSelf.runtimeAnimatorController = lightFlameColors[1];
                break;

            case Type.GreenLight:
                colorSelf.color = Color.green;
                flameSelf.runtimeAnimatorController = lightFlameColors[2];
                break;
        }
            
    }

    public void ChangePlayerLanternColor()
    {
        if (lwController.lighterEquipped == false)
        {
            if (!Input.GetMouseButton(1))
            { 
             if (colorSelf.color == Color.yellow)
                {
                    LanternYellow();
                }

                if (colorSelf.color == Color.blue)
                {
                    LanternBlue();
                }

                if (colorSelf.color == Color.green)
                {
                    LanternGreen();
                }
            }

           else if (Input.GetMouseButton(1))
            {
                colorSelf.color = lanternLightColor.color;
                var main = poofEffect.main;
             main.startColor = lanternLightColor.color;
                poofEffect.Play();
                flameSelf.runtimeAnimatorController = lanternFlameColor.runtimeAnimatorController;
            }

        }


    }

    void LanternYellow()
    {
        lanternFlameColor.runtimeAnimatorController = lightFlameColors[0];
        lanternLightColor.color = Color.yellow;
        var main = poofEffect.main;
        main.startColor = Color.yellow;
        poofEffect.Play();
    }

    void LanternBlue()
    {
        lanternFlameColor.runtimeAnimatorController = lightFlameColors[1];
        lanternLightColor.color = Color.blue;
        var main = poofEffect.main;
        main.startColor = Color.blue;
        poofEffect.Play();

        if (playedBlue == false)
        {
            playedBlue = true;
            lwController.blueCollected = true;
            lwController.buttonBlue.interactable = true;
            lwController.iconBlue.SetActive(true);
        }
    }

    void LanternGreen()
    {
        lanternFlameColor.runtimeAnimatorController = lightFlameColors[2];
        lanternLightColor.color = Color.green;
        var main = poofEffect.main;
        main.startColor = Color.green;
        poofEffect.Play();

        if (playedGreen == false)
        {
            playedGreen = true;
            lwController.greenCollected = true;
            lwController.buttonGreen.interactable = true;
            lwController.iconGreen.SetActive(true);
        }
    }

    enum Type
    {
        YellowLight,
        BlueLight,
        GreenLight
    };
}

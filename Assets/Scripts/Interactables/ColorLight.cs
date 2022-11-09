using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLight : MonoBehaviour
{
    [SerializeField]
    Type lightType = new Type();

    public Animator lanternFlameColor;
    public Light lanternLightColor;
    public LanternWheelController lwController;

    Light colorSelf;
    Animator flameSelf;
    SpriteRenderer spriteSelf;
    ParticleSystem poofEffect;

    public RuntimeAnimatorController[] lightFlameColors;

    bool playedBlue, playedGreen, playedRed;
    Color defaultLight;

    // Start is called before the first frame update
    void Start()
    {
        //lanternFlameColor = GameObject.FindWithTag("LanternFlame").GetComponent<Animator>();
        //lanternLightColor = GameObject.FindWithTag("LanternLight").GetComponent<Light>();
        //lwController = GameObject.FindWithTag("LanternWheel").GetComponent<LanternWheelController>();
        defaultLight = lwController.defaultColor;

        colorSelf = GetComponentInChildren<Light>();
        flameSelf = GetComponentInChildren<Animator>();
        spriteSelf = GetComponentInChildren<SpriteRenderer>();
        poofEffect = GetComponentInChildren<ParticleSystem>();

        switch (lightType)
        {
            case Type.YellowLight:
                colorSelf.color = defaultLight;
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
            case Type.RedLight:
                colorSelf.color = Color.red;
                flameSelf.runtimeAnimatorController = lightFlameColors[0];
                break;
            case Type.Off:
                colorSelf.enabled = false;
                spriteSelf.enabled = false;
                break;
        }
            
    }

    public void ChangePlayerLanternColor()
    {
        if (lwController.lighterEquipped == false)
        {
            colorSelf.enabled = true;
            spriteSelf.enabled = true;
            colorSelf.color = lanternLightColor.color;
            var main = poofEffect.main;
            main.startColor = lanternLightColor.color;
            poofEffect.Play();
            flameSelf.runtimeAnimatorController = lanternFlameColor.runtimeAnimatorController;

            //if (!Input.GetMouseButton(1))
            //{ 
            //    if (colorSelf.color == defaultLight)
            //    {
            //        LanternYellow();
            //    }

            //    if (colorSelf.color == Color.blue)
            //    {
            //        LanternBlue();
            //    }

            //    if (colorSelf.color == Color.green)
            //    {
            //        LanternGreen();
            //    }

            //    if (colorSelf.color == Color.red)
            //    {
            //        LanternRed();
            //    }
            //}
        }
    }

    void LanternYellow()
    {
        lanternFlameColor.runtimeAnimatorController = lightFlameColors[0];
        lanternLightColor.color = defaultLight;
        var main = poofEffect.main;
        main.startColor = defaultLight;
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

    void LanternRed()
    {
        lanternFlameColor.runtimeAnimatorController = lightFlameColors[0];
        lanternLightColor.color = Color.red;
        var main = poofEffect.main;
        main.startColor = Color.red;
        poofEffect.Play();

        if (playedRed == false)
        {
            playedRed = true;
            lwController.redCollected = true;
            lwController.buttonRed.interactable = true;
            lwController.iconRed.SetActive(true);
        }
    }

    enum Type
    {
        YellowLight,
        BlueLight,
        GreenLight,
        RedLight,
        Off
    };
}

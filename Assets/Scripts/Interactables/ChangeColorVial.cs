using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorVial : MonoBehaviour
{
    [SerializeField]
    Type lightType = new Type();

    //Animator lanternFlameColor;
    //Light lanternLightColor;

    int vialColor;
    Light vialLight;
    public Animator lanternArms;
    MeshRenderer meshRend;

    public RuntimeAnimatorController[] lightFlameColors;
    LanternWheelController lwController;

    bool playedBlue, playedGreen, playedRed;
    Color defaultLight;

    public AudioSource AudioToPlay;

    // Start is called before the first frame update
    void Start()
    {
        //lanternFlameColor = GameObject.Find("PlayerLanternFlame").GetComponent<Animator>();
        //lanternLightColor = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        lwController = GameObject.Find("LanternWheel").GetComponent<LanternWheelController>();
        //lanternArms = GameObject.Find("LanternArms").GetComponent<Animator>();
        meshRend = GetComponent<MeshRenderer>();
        vialLight = GetComponent<Light>();
        defaultLight = lwController.defaultColor;

        switch (lightType)
        {
            case Type.BlueVial:
                vialColor = 1;
                vialLight.color = Color.blue;
                break;

            case Type.GreenVial:
                vialColor = 2;
                vialLight.color = Color.green;
                break;
            case Type.RedVial:
                vialColor = 3;
                vialLight.color = Color.red;
                break;
        }

    }

    public void ChangePlayerLanternColor()
    {
        meshRend.enabled = false;
        vialLight.enabled = false;

        if (vialColor == 1)
        {
            LanternBlue();
        }

        if (vialColor == 2)
        {
            LanternGreen();
        }

        if (vialColor == 3)
        {
            LanternRed();
        }
    }

    void LanternBlue()
    {
        lwController.UnequipLighter();
        lanternArms.SetBool("Reload", true);
        Invoke("ReloadBlue", 1f);

        if (playedBlue == false)
        {
            playedBlue = true;
            lwController.blueCollected = true;
            lwController.buttonBlue.interactable = true;
            lwController.iconBlue.SetActive(true);
        }
    }
    void ReloadBlue()
    {
        lwController.ReloadBlue();
        Destroy(this.gameObject);
    }

    void LanternGreen()
    {
        lwController.UnequipLighter();
        lanternArms.SetBool("Reload", true);
        Invoke("ReloadGreen", 1f);

        if (playedGreen == false)
        {
            playedGreen = true;
            lwController.greenCollected = true;
            lwController.buttonGreen.interactable = true;
            lwController.iconGreen.SetActive(true);
        }
    }

    void ReloadGreen()
    {
        lwController.ReloadGreen();
        Destroy(this.gameObject);
    }

    void LanternRed()
    {
        lwController.UnequipLighter();
        lanternArms.SetBool("Reload", true);
        Invoke("ReloadRed", 1f);

        if (playedRed == false)
        {
            playedRed = true;
            lwController.redCollected = true;
            lwController.buttonRed.interactable = true;
            lwController.iconRed.SetActive(true);
        }
    }
    void ReloadRed()
    {
        lwController.ReloadRed();
        AudioToPlay.Play();
        Destroy(this.gameObject);
    }

    enum Type
    {
        BlueVial,
        GreenVial,
        RedVial
    };
}

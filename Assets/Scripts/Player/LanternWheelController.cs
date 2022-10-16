using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SUPERCharacter;

public class LanternWheelController : MonoBehaviour
{
    public bool CanInteract;

    Animator anim;
    private bool lanternWheelSelected = false;
    public static int lanternID;

    public SUPERCharacterAIO charScript;

    public RuntimeAnimatorController flameYellow, flameBlue, flameGreen;
    Animator flameColor;
    Light flameLight;
    GameObject arm;
    Animator armAnim;

    public bool blueCollected = false;
    public bool greenCollected = false;

    public Button buttonBlue;
    public GameObject iconBlue;
    public Button buttonGreen;
    public GameObject iconGreen;

    bool played = false;

    GameObject lighter;
    Animator lighterAnim;
    public bool lighterEquipped = true;
    bool lighterOn = false;

    [HideInInspector]
    public bool tutorial;
    public GameObject tutorialUI;

    public List<AudioClip> SoundEffects = new List<AudioClip>();
    private AudioSource Source;

    public AudioClip OpenUI;
    public AudioClip CloseUI;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
        buttonBlue.interactable = false;
        iconBlue.SetActive(false);
        buttonGreen.interactable = false;
        iconGreen.SetActive(false);

        anim = GetComponent<Animator>();
        flameLight = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        flameColor = GameObject.Find("PlayerLanternFlame").GetComponent<Animator>();
        arm = GameObject.Find("LanternArms");
        armAnim = arm.GetComponent<Animator>();

        lighter = GameObject.Find("Lighter");
        lighter.SetActive(false);
        lighterAnim = lighter.GetComponent<Animator>();
        arm.SetActive(false);
    }

    void Update()
    {
        if (CanInteract)
        {
            if(tutorial && Input.GetKeyDown(KeyCode.Q))
            {
                tutorialUI.SetActive(false);
                Time.timeScale = 1;
                tutorial = false;
            }

            if (Input.GetKeyDown(KeyCode.Q) && !played)
            {
                Source.clip = OpenUI;
                Source.Play();
                played = true;
                charScript.enableCameraControl = false;
                lanternWheelSelected = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyUp(KeyCode.Q) && played)
            {
                Source.clip = CloseUI;
                Source.Play();
                played = false;
                charScript.enableCameraControl = true;
                lanternWheelSelected = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }


        if (lanternWheelSelected)
        {
            anim.SetBool("OpenLanternWheel", true);
        }
        else
        {
            anim.SetBool("OpenLanternWheel", false);
        }   

        switch (lanternID)
        {
            case 0: //nothing
                break;
            case 1: //Standard Lantern Color
                UnequipLighter();
                armAnim.SetBool("Reload", true);
                Invoke("ReloadYellow", 1f);
                break;
            case 2: // Blue light
                if (blueCollected == true)
                {
                    UnequipLighter();
                    armAnim.SetBool("Reload", true);
                    Invoke("ReloadBlue", 1f);
                }
                break;
            case 3: // X light
                
                break;
            case 4: // X light

                break;
            case 5: // Lighter
                StartCoroutine(PlaySound(SoundEffects[3], 0.15f));
                armAnim.SetBool("Reload", true);
                Invoke("EquipLighter", 1f);
                break;
            case 6: // X light

                break;
            case 7: // X light

                break;
            case 8: // Green light
                if (greenCollected == true)
                {
                    UnequipLighter();
                    armAnim.SetBool("Reload", true);
                    Invoke("ReloadGreen", 1f);
                }
                break;

        }
    }

    void ReloadYellow()
    {
        StartCoroutine(PlaySound(SoundEffects[2], 0.3f));
        flameColor.runtimeAnimatorController = flameYellow;
        flameLight.color = Color.yellow;
        armAnim.SetBool("Reload", false);
    }

    void ReloadBlue()
    {
        StartCoroutine(PlaySound(SoundEffects[2], 0.3f));
        flameColor.runtimeAnimatorController = flameBlue;
        flameLight.color = Color.blue;
        armAnim.SetBool("Reload", false);
    }

    void ReloadGreen()
    {
        flameColor.runtimeAnimatorController = flameGreen;
        flameLight.color = Color.green;
        armAnim.SetBool("Reload", false);
    }

    public void EquipLighter()
    {
        lighterOn = true;
        lighterEquipped = true;
        arm.SetActive(false);
        lighter.SetActive(true);
        lighterAnim.SetBool("Equip", true);

        StartCoroutine(PlaySound(SoundEffects[0], 1));
    }

    void UnequipLighter()
    {
        if (lighterOn)
        {
            lighterOn = false;
            lighterAnim.SetBool("Equip", false);
            StartCoroutine(PlaySound(SoundEffects[1], 0.2f));
            Invoke("LighterOff", 1f);
            
        }
    }


    void LighterOff()
    {
        lighterEquipped = false;
        //StartCoroutine(PlaySound(SoundEffects[2], 0.3f));
        lighter.SetActive(false);
        arm.SetActive(true);
    }

    public void PickupLantern()
    {
        UnequipLighter();
    }

    IEnumerator PlaySound(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);

        Source.clip = clip;
        Source.Play();
    }
}

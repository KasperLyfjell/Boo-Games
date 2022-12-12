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
    public Animator flameColor;
    public Light flameLight;
    public GameObject arm;
    Animator armAnim;

    public bool blueCollected = false;
    public bool greenCollected = false;
    public bool redCollected = false;

    public Button buttonBlue;
    public GameObject iconBlue;
    public Button buttonGreen;
    public GameObject iconGreen;
    public Button buttonRed;
    public GameObject iconRed;

    bool played = false;

    public GameObject lighter;
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

    public Color defaultColor = new Color(0.945098f, 0.6392157f, 0.09411765f, 1f);

    public GameObject Pointer;
    private Vector3 MousePos;
    private Vector3 ScreenCenter;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
        buttonBlue.interactable = false;
        iconBlue.SetActive(false);
        buttonGreen.interactable = false;
        iconGreen.SetActive(false);
        buttonRed.interactable = false;
        iconRed.SetActive(false);

        anim = GetComponent<Animator>();
        if(flameLight == null)
            flameLight = GameObject.Find("PlayerLanternLight").GetComponent<Light>();
        if(flameColor == null)
            flameColor = GameObject.Find("PlayerLanternFlame").GetComponent<Animator>();
        if(arm == null)
            arm = GameObject.Find("LanternArms");
        armAnim = arm.GetComponent<Animator>();

        if(lighter == null)
            lighter = GameObject.Find("Lighter");
        lighter.SetActive(false);
        lighterAnim = lighter.GetComponent<Animator>();
        arm.SetActive(false);
    }

    void Update()
    {
        if (CanInteract)
        {
            PointerMovement();

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
            case 3: // Red light
                if (redCollected == true)
                {
                    UnequipLighter();
                    armAnim.SetBool("Reload", true);
                    Invoke("ReloadRed", 1f);
                }
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
        flameLight.color = defaultColor;
        flameLight.intensity = 30000;
        armAnim.SetBool("Reload", false);
    }

    public void ReloadBlue()
    {
        StartCoroutine(PlaySound(SoundEffects[2], 0.3f));
        flameColor.runtimeAnimatorController = flameBlue;
        flameLight.color = Color.blue;
        flameLight.intensity = 7500;
        armAnim.SetBool("Reload", false);
    }

    public void ReloadGreen()
    {
        StartCoroutine(PlaySound(SoundEffects[2], 0.3f));
        flameColor.runtimeAnimatorController = flameGreen;
        flameLight.color = Color.green;
        flameLight.intensity = 7500;
        armAnim.SetBool("Reload", false);
    }

    public void ReloadRed()
    {
        StartCoroutine(PlaySound(SoundEffects[2], 0.3f));
        flameColor.runtimeAnimatorController = flameYellow;
        flameLight.color = Color.red;
        flameLight.intensity = 10000;
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

    public void UnequipLighter()
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

    private void PointerMovement()
    {
        ScreenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
        MousePos = Input.mousePosition - ScreenCenter;
        MousePos.Normalize();

        float rotation_z = Mathf.Atan2(MousePos.y, MousePos.x) * Mathf.Rad2Deg;
        Quaternion Target = Quaternion.Euler(0, 0, rotation_z);

        Pointer.transform.rotation = Quaternion.RotateTowards(Pointer.transform.rotation, Target, Time.deltaTime * 700);
    }

    public void CollectBlue()
    {
        blueCollected = true;
    }
}

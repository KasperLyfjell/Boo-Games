using UnityEngine;
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

    [HideInInspector]
    public bool tutorial;
    public GameObject tutorialUI;

    private void Start()
    {
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
                played = true;
                charScript.enableCameraControl = false;
                lanternWheelSelected = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyUp(KeyCode.Q) && played)
            {
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
        flameColor.runtimeAnimatorController = flameYellow;
        flameLight.color = Color.yellow;
        armAnim.SetBool("Reload", false);
    }

    void ReloadBlue()
    {
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
        lighterEquipped = true;
        arm.SetActive(false);
        lighter.SetActive(true);
        lighterAnim.SetBool("Equip", true);
    }

    void UnequipLighter()
    {
        if (lighterEquipped)
        {
            lighterEquipped = false;
            lighterAnim.SetBool("Equip", false);
            Invoke("LighterOff", 1f);
            
        }
    }

    void LighterOff()
    {
        lighter.SetActive(false);
        arm.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void PickupLantern()
    {
        UnequipLighter();
    }
}

using UnityEngine;
using UnityEngine.UI;
using SUPERCharacter;

public class LanternWheelController : MonoBehaviour
{
    public Animator anim;
    private bool lanternWheelSelected = false;
    public static int lanternID;

    public SUPERCharacterAIO charScript;

    public Animator flameColor;
    public RuntimeAnimatorController flameYellow;
    public RuntimeAnimatorController flameBlue;
    public RuntimeAnimatorController flameGreen;
    public Light flameLight;

    public bool blueCollected = false;
    public bool greenCollected = false;
    public Button buttonBlue;
    public GameObject iconBlue;
    public Button buttonGreen;
    public GameObject iconGreen;

    private void Start()
    {
        buttonBlue.interactable = false;
        iconBlue.SetActive(false);
        buttonGreen.interactable = false;
        iconGreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            charScript.enableCameraControl = false;
            lanternWheelSelected = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            charScript.enableCameraControl = true;
            lanternWheelSelected = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
                flameColor.runtimeAnimatorController = flameYellow;
                flameLight.color = Color.yellow;
                break;
            case 2: // Blue light
                if (blueCollected == true)
                {
                    flameColor.runtimeAnimatorController = flameBlue;
                    flameLight.color = Color.blue;
                }
                break;
            case 3: // X light
                
                break;
            case 4: // X light

                break;
            case 5: // X light

                break;
            case 6: // X light

                break;
            case 7: // X light

                break;
            case 8: // Green light
                if (greenCollected == true)
                {
                    flameColor.runtimeAnimatorController = flameGreen;
                    flameLight.color = Color.green;
                }
                break;

        }
    }
}

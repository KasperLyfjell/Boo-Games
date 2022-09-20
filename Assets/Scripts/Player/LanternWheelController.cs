using UnityEngine;
using UnityEngine.UI;

public class LanternWheelController : MonoBehaviour
{
    public Animator anim;
    private bool lanternWheelSelected = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            lanternWheelSelected = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;

public class Env_Door : MonoBehaviour
{
    private bool CanInteract;

    private GameObject player;

    private float MinRotation;
    private float MaxRotation;
    private float CurrentRotation;
    private float DoorPercentage;

    private float cursorPos;
    private float cursorCenter;
    private float maxCursorDistance;

    private float opening;
    private float lastOpeningpos;

    public TextMeshProUGUI InteractCue;

    private KeyCode DoorInteract;


    void Start()
    {
        //Cursor.visible = false;//This is unnecessary, delete when merging and do it in the character model instead (the cursor cannot be blocked)

        CurrentRotation = transform.eulerAngles.y;
        MinRotation = CurrentRotation;
        MaxRotation = CurrentRotation + 90;

        DoorInteract = KeyCode.Mouse0;//This is a test function that's modifiable later on
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition.x);
        Debug.Log(CurrentRotation);

        if (CanInteract)
        {
            InteractCue.text = "Press " + DoorInteract;

            if (Input.GetKeyDown(DoorInteract))
            {
                cursorPos = Input.mousePosition.x;
                cursorCenter = cursorPos;
                maxCursorDistance = cursorPos * 2;
            }

            if (Input.GetKey(DoorInteract))
                Interact();
            else
            {
                player.GetComponent<SUPERCharacterAIO>().enableCameraControl = true;
                Cursor.lockState = CursorLockMode.Locked;
                InteractCue.gameObject.SetActive(true);
                if (opening < 0)
                    lastOpeningpos = 0;
                else if (opening > 90)
                    lastOpeningpos = 90;
                else
                    lastOpeningpos = opening;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanInteract = true;
            player = other.gameObject;
            InteractCue.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
            InteractCue.gameObject.SetActive(false);
        }
    }


    private void Interact()
    {
        InteractCue.gameObject.SetActive(false);
        player.GetComponent<SUPERCharacterAIO>().enableCameraControl = false;
        Cursor.lockState = CursorLockMode.Confined;
        cursorPos = Input.mousePosition.x;

        opening = ((cursorPos - cursorCenter) / maxCursorDistance);
        opening *= 180; //might be too weak, needs tweaking
        opening += lastOpeningpos;
        Debug.Log(opening);

        if(CurrentRotation + opening < MaxRotation && CurrentRotation + opening > MinRotation)
            transform.eulerAngles = new Vector3(transform.rotation.x, CurrentRotation + opening, transform.rotation.z);
    }
}
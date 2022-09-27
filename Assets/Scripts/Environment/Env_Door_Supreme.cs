using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;

public class Env_Door_Supreme : MonoBehaviour
{
    [Header("DONT CHILD THIS OBJECT TO ANYTHING OTHER THAN DOORFORM")]
    /* The door is sensitive to its own rotation and needs to accurately
     * read its current euler angles to function. Note that the door can
     * be childed to a parent ONLY IF the parent has a 0,0,0 position and 0,0,0 rotation.
     * Recommend to child the door to a specific 'doors' parent to keep the hierarchy clean.
     */

    #region Public variables
    [Tooltip("How wide should the door open? (Recommend 90*)")]
    public float MaximumOpenRange;

    [Tooltip("The baseline speed at which the player opens the door. This is the minimum speed. (Recommend ~10)")]
    public float OpeningSpeed;
    [Tooltip("How much effect does pulling on the door have. If you yank the mouse, the door will open much faster with higher force. (Recommend ~250)")]
    public float OpeningForce;

    public TextMeshProUGUI InteractCue;

    #endregion

    #region Private variables
    private bool CanInteract;
    private bool OutOfBoundries;

    private GameObject player;

    private float MinRotation;
    private float MaxRotation;
    private float CurrentRotation;
    private float OpeningPercentage;

    private float cursorPos;
    private float cursorCenter;
    private float maxCursorDistance;

    private float opening;

    private KeyCode DoorInteract;

    private Quaternion RotationalPosition;

    private float force;
    private float decay;
    #endregion

    #region Audio
    private AudioSource SFX;

    public AudioClip OpeningFX;
    public AudioClip SlowOpeningFX;
    //public AudioClip ClosingFX;//Same audio file but in reverse

    private float clipLength;
    private float startTime;
    private float soundChangeThreshold;

    #endregion


    void Start()
    {
        CurrentRotation = transform.localEulerAngles.y;//this is supposed to be 0 but isnt because of local angles. Fix tomorrow
        MinRotation = CurrentRotation;
        MaxRotation = CurrentRotation + MaximumOpenRange;
        Debug.Log("Current Rot: " + CurrentRotation + ", Max Rot: " + MaxRotation);

        DoorInteract = KeyCode.Mouse0;//This is a test function that's modifiable later on

        SFX = GetComponent<AudioSource>();
        clipLength = SFX.clip.length;

        RotationalPosition = transform.localRotation;
        soundChangeThreshold = OpeningSpeed + (OpeningForce / 10);//Should be hopefully around 40
    }

    private void Update()
    {
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
                CurrentRotation = transform.localEulerAngles.y;
            }
        }

        if (transform.localRotation != RotationalPosition)
        {
            decay = RotationalPosition.y - transform.localRotation.y;
            if (decay < 0)
                decay *= -1;
            decay *= OpeningForce;
            //decay will decrease as the door gets closer to its final position. Decay is positive as to avoid issues if force would become negative. 
            force = OpeningSpeed + decay;
            //Debug.Log("Force: " + force + ", Decay: " + decay);//Nice dubugging tool to figure out how the physics work

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, RotationalPosition, force * Time.deltaTime);

            if (transform.localEulerAngles.y > MaxRotation)//Slamming door open
            {
                if(RotationalPosition.y > transform.localEulerAngles.y)
                {
                    //find out the strength of the slam to play sound    
                }

                OutOfBoundries = true;
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, MaxRotation, transform.localRotation.z);
            }
            else if (transform.localEulerAngles.y < MinRotation)//Locking the door
            {
                if(RotationalPosition.y < transform.localEulerAngles.y)
                {
                    //find out the strength of the slam to play sound
                }

                OutOfBoundries = true;
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, MinRotation, transform.localRotation.z);
            }
            else
            {
                OutOfBoundries = false;
                AudioSetup();
            }
        }
        else
            SFX.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
        Cursor.lockState = CursorLockMode.None;
        cursorPos = Input.mousePosition.x;

        opening = ((cursorPos - cursorCenter) / maxCursorDistance); //takes into consideration difference in different screen sizes
        opening *= MaxRotation * 2;

        /*
        if (CurrentRotation + opening > MaxRotation)
        {
            opening = MaxRotation - CurrentRotation;
        }
        if (CurrentRotation + opening < MinRotation)
        {
            opening = MinRotation - CurrentRotation;
        }
        */

        Vector3 rotPos = new Vector3(transform.localRotation.x, CurrentRotation + opening, transform.localRotation.z);
        RotationalPosition = Quaternion.Euler(rotPos);
    }

    private void AudioSetup()
    {
        OpeningPercentage = ((transform.localEulerAngles.y - MinRotation) / (MaxRotation - MinRotation));//Calculates how wide the door is opened in percentages from closed (0%) to fully open (100%)


        if (RotationalPosition.eulerAngles.y > transform.localEulerAngles.y)//The door is opening
        {
            //Change to opening audio
        }
        else if (RotationalPosition.eulerAngles.y < transform.localEulerAngles.y)//The door is closing
        {
            OpeningPercentage = 1 - OpeningPercentage;
            //Change to closing audio
        }


        startTime = clipLength * OpeningPercentage;

        if (!SFX.isPlaying)
        {
            PlaySound(startTime);
        }
        else if (force < soundChangeThreshold && SFX.clip != SlowOpeningFX)
        {
            ChangeSound(SlowOpeningFX);
        }
        else if (force > soundChangeThreshold && SFX.clip == SlowOpeningFX)
        {
            ChangeSound(OpeningFX);
        }
    }

    private void PlaySound(float start)
    {
        SFX.time = start;
        SFX.Play();
    }

    private void ChangeSound(AudioClip sound)
    {
        SFX.Stop();
        SFX.clip = sound;
        clipLength = SFX.clip.length;
        startTime = clipLength * OpeningPercentage;
        PlaySound(startTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;

public class Env_Door: MonoBehaviour
{
    [Header("DONT CHILD THIS OBJECT TO ANYTHING")]
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
        CurrentRotation = transform.eulerAngles.y;
        MinRotation = CurrentRotation;
        MaxRotation = CurrentRotation + MaximumOpenRange;

        DoorInteract = KeyCode.Mouse0;//This is a test function that's modifiable later on

        SFX = GetComponent<AudioSource>();
        clipLength = SFX.clip.length;

        RotationalPosition = transform.rotation;
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
                CurrentRotation = transform.eulerAngles.y;
            }
        }

        if (transform.rotation != RotationalPosition)
        {
            float decay = RotationalPosition.y - transform.rotation.y;
            if (decay < 0)
                decay *= -1;
            decay *= OpeningForce;
            //decay will decrease as the door gets closer to its final position. Decay is positive as to avoid issues if force would become negative. 
            float force = OpeningSpeed + decay;
            //Debug.Log("Force: " + force + ", Decay: " + decay);//Nice dubugging tool to figure out how the physics work

            transform.rotation = Quaternion.RotateTowards(transform.rotation, RotationalPosition, force * Time.deltaTime);

            if (transform.eulerAngles.y > MaxRotation && RotationalPosition.y > transform.eulerAngles.y)//Slamming door open
            {
                //find out the strength of the slam to play sound

            }
            else if (transform.eulerAngles.y < MinRotation && RotationalPosition.y < transform.eulerAngles.y)//Locking the door
            {

            }

            OpeningPercentage = ((transform.eulerAngles.y - MinRotation) / (MaxRotation - MinRotation));//Calculates how wide the door is opened in percentages from closed (0%) to fully open (100%)

            if (RotationalPosition.eulerAngles.y > transform.eulerAngles.y)//The door is opening
            {
                //Change to opening audio
            }
            else if (RotationalPosition.eulerAngles.y < transform.eulerAngles.y)//The door is closing
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



        if (CurrentRotation + opening > MaxRotation)
        {
            opening = MaxRotation - CurrentRotation;
        }
        if (CurrentRotation + opening < MinRotation)
        {
            opening = MinRotation - CurrentRotation;
        }

        Vector3 rotPos = new Vector3(transform.rotation.x, CurrentRotation + opening, transform.rotation.z);
        RotationalPosition = Quaternion.Euler(rotPos);
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
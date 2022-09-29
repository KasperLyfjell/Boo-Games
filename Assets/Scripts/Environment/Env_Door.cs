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

    [Tooltip("The baseline speed at which the player opens the door. This is the minimum speed. (Recommend ~15)")]
    public float OpeningSpeed;
    [Tooltip("How much effect does pulling on the door have. If you yank the mouse, the door will open much faster with higher force. (Recommend ~300)")]
    public float OpeningForce;
    [Tooltip("How much force is required to slam the door open/close (Recommend ~80)")]
    public float SlamThreshold;

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
    private Vector3 RotationalAngle;

    private float decay;
    private float force;
    private bool OutOfBoundries;
    #endregion

    #region Audio
    private AudioSource SFX;

    public AudioClip OpeningFX;
    public AudioClip SlowOpeningFX;

    public AudioClip ClosingFX;
    public AudioClip SlowClosingFX;

    public AudioClip SlamOpen;
    public AudioClip SlamClose;
    public AudioClip QuietClose;
    //public AudioClip ClosingFX;//Same audio file but in reverse

    private float clipLength;
    private float startTime;
    private float soundChangeThreshold;

    #endregion


    void Start()
    {
        CurrentRotation = transform.EulerAsInspector().y;
        MinRotation = CurrentRotation;
        MaxRotation = CurrentRotation + MaximumOpenRange;

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
                CurrentRotation = transform.EulerAsInspector().y;
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

            if (transform.EulerAsInspector().y > MaxRotation && RotationalAngle.y > transform.EulerAsInspector().y)//Slamming door open
            {
                SFX.Stop();

                if (!OutOfBoundries)
                {
                    if (force > SlamThreshold)
                        Debug.Log("I slam the door open");

                    OutOfBoundries = true;
                }

                RotationalPosition = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));

            }
            else if (transform.EulerAsInspector().y < MinRotation && RotationalAngle.y  < transform.EulerAsInspector().y)//Locking the door
            {
                SFX.Stop();

                if (!OutOfBoundries)
                {
                    if (force > SlamThreshold)
                        Debug.Log("I slam the door close");
                    else
                        Debug.Log("I gently close the door");

                    OutOfBoundries = true;
                }

                RotationalPosition = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, RotationalPosition, force * Time.deltaTime);
                AudioSetup();
                OutOfBoundries = false;
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

        RotationalAngle = new Vector3(transform.rotation.x, CurrentRotation + opening, transform.localRotation.z);
        RotationalPosition = Quaternion.Euler(RotationalAngle);
    }


    private void AudioSetup()
    {
        OpeningPercentage = ((transform.EulerAsInspector().y - MinRotation) / (MaxRotation - MinRotation));//Calculates how wide the door is opened in percentages from closed (0%) to fully open (100%)

        if (RotationalPosition.eulerAngles.y > transform.eulerAngles.y)//The door is opening
        {
            SFX.clip = OpeningFX;
        }
        else if (RotationalPosition.eulerAngles.y < transform.eulerAngles.y)//The door is closing
        {
            OpeningPercentage = 1 - OpeningPercentage;
            SFX.clip = ClosingFX;
        }

        startTime = clipLength * OpeningPercentage;

        if (!SFX.isPlaying)
        {
            PlaySound(startTime);
        }
        else if (force < soundChangeThreshold)//Slow sfx
        {
            if (RotationalPosition.eulerAngles.y > transform.eulerAngles.y && SFX.clip != SlowOpeningFX)//opening
            {
                ChangeSound(SlowOpeningFX);
            }
            else if(RotationalPosition.eulerAngles.y < transform.eulerAngles.y && SFX.clip != SlowClosingFX)//closing
            {
                ChangeSound(SlowClosingFX);
            }
        }
        else if (force > soundChangeThreshold)//Fast sfx
        {
            if (RotationalPosition.eulerAngles.y > transform.eulerAngles.y && SFX.clip != OpeningFX)//opening
            {
                ChangeSound(OpeningFX);
            }
            else if (RotationalPosition.eulerAngles.y < transform.eulerAngles.y && SFX.clip != ClosingFX)//closing
            {
                ChangeSound(ClosingFX);
            }
        }
        /*
        else if (force < soundChangeThreshold && SFX.clip != SlowOpeningFX)
        {
            ChangeSound(SlowOpeningFX);
        }
        else if (force > soundChangeThreshold && SFX.clip == SlowOpeningFX)
        {
            ChangeSound(OpeningFX);
        }
        */
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;

public class Env_Door: MonoBehaviour
{
    #region Public variables
    public bool IsLocked;

    [Tooltip("How wide should the door open? (Recommend 90*)")]
    public float MaximumOpenRange;

    [Tooltip("The baseline speed at which the player opens the door. This is the minimum speed. (Recommend ~15)")]
    public float OpeningSpeed;
    [Tooltip("How much effect does pulling on the door have. If you yank the mouse, the door will open much faster with higher force. (Recommend ~300)")]
    public float OpeningForce;
    [Tooltip("How much force is required to slam the door open/close (Recommend ~80)")]
    public float SlamThreshold;
    [Tooltip("The volume of the door opening and closing (does not impact the slamming and closing of the door sounds)")]
    public float StandardVolume;

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
        SFX.volume = StandardVolume;

        RotationalPosition = transform.localRotation;
        soundChangeThreshold = OpeningSpeed + (OpeningForce / 10);//Should be hopefully around 40
    }

    private void Update()
    {
        if (CanInteract && !IsLocked)
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
                if (!OutOfBoundries)
                {
                    SFX.Stop();

                    if (force > SlamThreshold)
                    {
                        SFX.volume = (0.3f + force / 350);
                        SlamSound(SlamOpen);
                    }

                    OutOfBoundries = true;
                }

                RotationalPosition = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z));

            }
            else if (transform.EulerAsInspector().y < MinRotation && RotationalAngle.y  < transform.EulerAsInspector().y)//Locking the door
            {
                if (!OutOfBoundries)
                {
                    SFX.Stop();

                    if (force > SlamThreshold)
                    {
                        SFX.volume = (0.3f + force / 350);
                        SlamSound(SlamClose);
                    }
                    else
                    {
                        SFX.volume = (0.2f + force / 100);
                        SlamSound(QuietClose);
                    }

                    OutOfBoundries = true;
                }

                RotationalPosition = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z));
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, RotationalPosition, force * Time.deltaTime);
                if(force >= 16)
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


        RotationalAngle = new Vector3(transform.localRotation.x, CurrentRotation + opening, transform.localRotation.z);
        RotationalPosition = Quaternion.Euler(RotationalAngle);
    }


    private void AudioSetup()
    {
        OpeningPercentage = ((transform.EulerAsInspector().y - MinRotation) / (MaxRotation - MinRotation));//Calculates how wide the door is opened in percentages from closed (0%) to fully open (100%)

        if (RotationalPosition.eulerAngles.y < transform.localEulerAngles.y)//The door is closing
        {
            OpeningPercentage = 1 - OpeningPercentage;
        }
        Debug.Log(OpeningPercentage);
        startTime = clipLength * OpeningPercentage;

        if(startTime <= clipLength && startTime >= 0)//removes annoying errors in editor, however the script works fine without this line
        {
            if (!SFX.isPlaying)
            {
                PlaySound(startTime);
            }
            if (force < soundChangeThreshold)//Slow sfx
            {
                if (RotationalPosition.eulerAngles.y > transform.localEulerAngles.y && SFX.clip != SlowOpeningFX)//opening
                {
                    ChangeSound(SlowOpeningFX);
                }
                else if (RotationalPosition.eulerAngles.y < transform.localEulerAngles.y && SFX.clip != SlowClosingFX)//closing
                {
                    ChangeSound(SlowClosingFX);
                }
            }
            else if (force > soundChangeThreshold)//Fast sfx
            {
                if (RotationalPosition.eulerAngles.y > transform.localEulerAngles.y && SFX.clip != OpeningFX)//opening
                {
                    ChangeSound(OpeningFX);
                }
                else if (RotationalPosition.eulerAngles.y < transform.localEulerAngles.y && SFX.clip != ClosingFX)//closing
                {
                    ChangeSound(ClosingFX);
                }
            }
        }
    }

    private void PlaySound(float start)
    {
        SFX.volume = StandardVolume;
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

    private void SlamSound(AudioClip sound)
    {
        SFX.clip = sound;
        SFX.Play();
    }
}
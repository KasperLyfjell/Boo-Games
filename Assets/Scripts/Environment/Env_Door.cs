using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;

public class Env_Door : MonoBehaviour
{
    #region Public variables
    [Tooltip("How wide should the door open? (Recommend 90*)")]
    public float MaximumOpenRange;

    public float OpeningSpeed;

    public TextMeshProUGUI InteractCue;

    #endregion

    #region Private variables
    private bool CanInteract;

    private GameObject player;

    private float MinRotation;
    private float MaxRotation;
    private float CurrentRotation;
    private float DoorChange;
    private float OpeningPercentage;

    private float cursorPos;
    private float cursorCenter;
    private float maxCursorDistance;

    private float opening;
    private float lastOpeningpos;


    private KeyCode DoorInteract;
    #endregion

    #region Audio
    private AudioSource SFX;

    public AudioClip OpeningFX;
    public AudioClip ClosingFX;//Same audio file but in reverse

    private float clipLength;
    private float currentClipTime;
    private float startTime;
    private float endTime;
    private float playbackSpeed;

    #endregion


    void Start()
    {
        CurrentRotation = transform.eulerAngles.y;
        MinRotation = CurrentRotation;
        MaxRotation = CurrentRotation + MaximumOpenRange;

        DoorInteract = KeyCode.Mouse0;//This is a test function that's modifiable later on

        SFX = GetComponent<AudioSource>();
        clipLength = SFX.clip.length;
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



            if (currentClipTime != endTime)//Only works for opening door
            {
                if (SFX.isPlaying)
                    startTime = SFX.time;
                else
                    startTime = currentClipTime;

                playbackSpeed = 0.7f;
                playbackSpeed += ((endTime - startTime) / (clipLength * 3));

                PlaySound(startTime, endTime, playbackSpeed);
            }
        }
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
        OpeningPercentage = ((transform.eulerAngles.y - MinRotation) / (MaxRotation - MinRotation));//Calculates how wide the door is opened in percentages from closed (0%) to fully open (100%)


        DoorChange = opening;
        InteractCue.gameObject.SetActive(false);
        player.GetComponent<SUPERCharacterAIO>().enableCameraControl = false;
        Cursor.lockState = CursorLockMode.None;
        cursorPos = Input.mousePosition.x;

        opening = ((cursorPos - cursorCenter) / maxCursorDistance); //takes into consideration difference in different screen sizes
        opening *= 180;
        //opening *= OpeningSpeed;



        if (CurrentRotation + opening > MaxRotation)
        {
            opening = MaxRotation - CurrentRotation;
        }
        if (CurrentRotation + opening < MinRotation)
        {
            opening = MinRotation - CurrentRotation;
        }

        transform.eulerAngles = new Vector3(transform.rotation.x, CurrentRotation + opening, transform.rotation.z);

        if (DoorChange != opening)
        {
            if (DoorChange < opening)//Triggers when the player moves the door
            {
                SFX.clip = OpeningFX;

                endTime = clipLength * OpeningPercentage; //To which segment should the audio play. If the door is opened to from 0% - 40%, the audio will play the same length
            }
            else if (DoorChange > opening)
            {
                SFX.clip = ClosingFX;

                endTime = clipLength * (1 - OpeningPercentage);
            }
        }
    }


    private void PlaySound(float start, float end, float speed)
    {
        SFX.pitch = speed;
        SFX.time = start;
        SFX.Play();
        SFX.SetScheduledEndTime(AudioSettings.dspTime + (end - start));
        currentClipTime = end;
    }
}
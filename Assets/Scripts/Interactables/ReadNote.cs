using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class ReadNote : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private Camera cam;
    private SUPERCharacterAIO player;
    private bool isHeld;
    private float smooth = 6;

    public GameObject Blur;
    public GameObject Text;
    public GameObject ReadPrompt;
    private bool isReading;
    public AudioSource sfxPlay;
    public AudioClip clickRead;

    public AudioTrigger PlaySpecial;
    private bool playOnce;

    private void Start()
    {
        cam = Camera.main;
        player = cam.GetComponentInParent<SUPERCharacterAIO>();

        //Physics.IgnoreCollision(GetComponent<Collider>(), cam.gameObject.GetComponentInParent<Collider>());
        Physics.IgnoreLayerCollision(0, 3);
        Physics.IgnoreLayerCollision(2, 3);

        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }

    private void Update()
    {
        if(isHeld)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                sfxPlay.clip = clickRead;
                sfxPlay.Play();
                ReadText();
            }

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 1.2f), Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, 180, 0), Time.deltaTime * smooth * 1.5f);
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, startRot, Time.deltaTime * smooth * 1.5f);
        }
    }


    public void PickupNote()
    {
        if (!isReading)
        {
            if (isHeld)
            {
                if(PlaySpecial != null && !playOnce)
                {
                    PlaySpecial.PlaySound();
                    playOnce = true;
                }

                transform.parent = null;

                player.enableCameraControl = true;
                player.enableMovementControl = true;
                ReadPrompt.SetActive(false);

                isHeld = false;
            }
            else
            {
                transform.parent = cam.gameObject.transform;

                player.enableCameraControl = false;
                player.enableMovementControl = false;
                ReadPrompt.SetActive(true);

                isHeld = true;
            }
        }
    }

    public void ReadText()
    {
        if (!isReading)
        {
            Blur.SetActive(true);
            Text.SetActive(true);
            isReading = true;
        }
        else
        {
            Blur.SetActive(false);
            Text.SetActive(false);
            isReading = false;
        }
    }
}
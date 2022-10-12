using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_Door : MonoBehaviour
{
    public KeyCode Interaction;
    public bool DoorIsLocked;

    private bool IsInside;

    public bool HasKey;

    private AudioSource Source;
    public AudioClip SFXLocked;
    public AudioClip SFXOpen;
    public AudioClip SFXUnlock;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsInside)
        {
            if (Input.GetKeyDown(Interaction))
            {
                if (HasKey)
                    Unlock();
                else
                {
                    if (DoorIsLocked)
                        Locked();
                    else
                        Open();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            IsInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            IsInside = false;
    }

    private void Locked()
    {
        Source.clip = SFXLocked;
        Source.Play();
    }

    private void Open()
    {
        Source.clip = SFXOpen;
        Source.Play();

        //Start fade to end-game
    }

    private void Unlock()
    {
        Source.clip = SFXUnlock;
        Source.Play();

        DoorIsLocked = false;
        HasKey = false;
    }
}

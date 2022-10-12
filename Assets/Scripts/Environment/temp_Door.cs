using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("This is for the ending")]
    private bool EndHasCome;
    public GameObject Dimmer;
    public AudioSource voice;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (EndHasCome)
        {
            Dimmer.GetComponent<Image>().color += new Color(0, 0, 0, 0.5f * Time.deltaTime);         
        }
        else
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
        if(SFXLocked != null)
        {
            Source.clip = SFXLocked;
            Source.Play();
        }
    }

    private void Open()
    {
        if (SFXOpen != null)
        {
            Source.clip = SFXOpen;
            Source.Play();

            StartCoroutine(Ending());
        }
    }

    private void Unlock()
    {
        if (SFXUnlock != null)
        {
            Source.clip = SFXUnlock;
            Source.Play();

            DoorIsLocked = false;
            HasKey = false;
        }
    }

    IEnumerator Ending()
    {
        EndHasCome = true;
        yield return new WaitForSeconds(4);
        voice.Play();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");
    }
}

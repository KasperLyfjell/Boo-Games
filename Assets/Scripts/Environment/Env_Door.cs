using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Env_Door : MonoBehaviour
{
    private bool CanInteract;

    private float MinRotation;
    private float MaxRotation;
    private float CurrentRotation;

    public TextMeshProUGUI InteractCue;

    void Start()
    {
        CurrentRotation = transform.rotation.y;
        MinRotation = CurrentRotation;
        MaxRotation = CurrentRotation + 90;
    }

    private void Update()
    {
        if (CanInteract)
        {
            InteractCue.gameObject.SetActive(true);
        }
        else
        {
            InteractCue.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
        }
    }
}

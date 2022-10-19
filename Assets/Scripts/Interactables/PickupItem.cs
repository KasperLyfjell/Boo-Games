using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupItem : MonoBehaviour
{
    [Tooltip("Found under: Audio > SFX > Collectable SFX player")]
    public AudioSource SFXListener;
    [SerializeField] ObjectType Type = new ObjectType();

    public KeyCode InteractButton;
    public AudioClip SoundEffect;

    private bool Inside;
    public GameObject tooltip;

    [Header("Only for Collectable")]
    public GameObject Tutorial;
    public UnityEvent addColor;
    [Header("Only for Lantern and Collectable")]
    public LanternWheelController LanternWheel;
    [Header("Only for Keys")]
    public temp_Door DoorToUnlock;

    [Header("If there is VA line when picking up item")]
    public AudioSource Voiceline;

    enum ObjectType
    {
        Collectable,
        Interactable,
        Lantern,
        Key
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Inside = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Inside = false;
        }
    }

    private void Update()
    {
        if (Inside)
        {
            if(tooltip != null)
                tooltip.SetActive(true);

            if (Input.GetKeyDown(InteractButton))
            {
                if(GetComponent<MeshRenderer>() != null)
                {
                    if(GetComponent<MeshRenderer>().enabled)
                        PickUp();
                }
                else
                    PickUp();
            }
        }
        else if (tooltip != null)
            tooltip.SetActive(false);
    }



    private void PickUp()
    {
        SFXListener.clip = SoundEffect;
        SFXListener.Play();

        if (Voiceline != null)
            Voiceline.Play();

        if(tooltip != null)
            Destroy(tooltip);

        switch (Type)
        {
            case ObjectType.Collectable:
                Tutorial.SetActive(true);
                LanternWheel.tutorial = true;
                LanternWheel.CanInteract = true;
                LanternWheel.blueCollected = true;
                addColor.Invoke();
                Time.timeScale = 0;
                Destroy(gameObject);
                break;
            case ObjectType.Interactable:
                //Here put in the look-at code if we want one
                break;
            case ObjectType.Lantern:
                LanternWheel.PickupLantern();
                Destroy(gameObject);
                break;
            case ObjectType.Key:
                if (GetComponent<MeshRenderer>().enabled)
                {
                    DoorToUnlock.HasKey = true;
                    Destroy(gameObject);
                }
                break;
        }
    }
}
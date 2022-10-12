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

    [Header("Only for Lantern")]
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
            if (Input.GetKeyDown(InteractButton))
            {
                PickUp();
            }
        }
    }



    private void PickUp()
    {
        SFXListener.clip = SoundEffect;
        SFXListener.Play();

        if (Voiceline != null)
            Voiceline.Play();

        switch (Type)
        {
            case ObjectType.Collectable:
                //Add object to inventory
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
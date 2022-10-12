using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] ObjectType Type = new ObjectType();

    public KeyCode InteractButton;
    public AudioSource SoundEffect;

    private GameObject player;
    private bool Inside;

    enum ObjectType
    {
        Collectable,
        Interactable,
        Lantern
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            player = collider.gameObject;
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
                //Trigger the "swap to lantern" animation
                break;
        }

        if(SoundEffect != null)
            if(SoundEffect.clip != null)
                SoundEffect.Play();

    }
}

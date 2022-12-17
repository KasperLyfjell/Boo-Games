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
            if(Input.GetKeyDown(KeyCode.F))
                ReadText();

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 1), Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * smooth * 1.5f);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, startRot, Time.deltaTime * smooth * 1.5f);
        }
    }


    public void PickupNote()
    {
        if (isHeld)
        {
            transform.parent = null;

            player.enableCameraControl = true;
            player.enableMovementControl = true;

            isHeld = false;
        }
        else
        {
            transform.parent = cam.gameObject.transform;

            player.enableCameraControl = false;
            player.enableMovementControl = false;

            isHeld = true;
        }
    }

    public void DropNote()
    {

    }

    public void ReadText()
    {
        Debug.Log("Show text");
    }
}
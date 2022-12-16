using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    Rigidbody playerRigidbody;

    float defaultPosY = 0;
    float timer = 0;

    public bool bobbingZaxis;
    float defaultPosZ = 0;
    float timerZ = 0;
    public float walkingBobbingSpeedZ = 14f;
    public float bobbingAmountZ = 0.05f;

    //sprint speed, but badly written sorry
    float Sprint_walkingBobbingSpeed;
    float Sprint_wbobbingAmount;
    float Sprint_wwalkingBobbingSpeedZ;
    float Sprint_wbobbingAmountZ;

    float Normal_walkingBobbingSpeed;
    float Normal_wbobbingAmount;
    float Normal_wwalkingBobbingSpeedZ;
    float Normal_wbobbingAmountZ;

    [HideInInspector] public bool OverrideBobbing;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
        defaultPosY = transform.localPosition.y;
        defaultPosZ = transform.localPosition.z;

        Sprint_walkingBobbingSpeed = walkingBobbingSpeed * 1.5f;
        Sprint_wbobbingAmount = bobbingAmount * 1.5f;
        Sprint_wwalkingBobbingSpeedZ = walkingBobbingSpeedZ * 1.5f;
        Sprint_wbobbingAmountZ = bobbingAmountZ * 1.5f;

        Normal_walkingBobbingSpeed = walkingBobbingSpeed;
        Normal_wbobbingAmount = bobbingAmount;
        Normal_wwalkingBobbingSpeedZ = walkingBobbingSpeedZ;
        Normal_wbobbingAmountZ = bobbingAmountZ;
    }

    // Update is called once per frame

    void Update()
    {
        if (!OverrideBobbing)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                walkingBobbingSpeed = Sprint_walkingBobbingSpeed;
                bobbingAmount = Sprint_wbobbingAmount;
                walkingBobbingSpeedZ = Sprint_wwalkingBobbingSpeedZ;
                bobbingAmountZ = Sprint_wbobbingAmountZ;
            }
            else
            {
                walkingBobbingSpeed = Normal_walkingBobbingSpeed;
                bobbingAmount = Normal_wbobbingAmount;
                walkingBobbingSpeedZ = Normal_wwalkingBobbingSpeedZ;
                bobbingAmountZ = Normal_wbobbingAmountZ;
            }

            if (playerRigidbody.velocity.magnitude > 2f)
            {
                //Player is moving
                timer += Time.deltaTime * walkingBobbingSpeed;
                transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);

                if (bobbingZaxis)
                {
                    timerZ += Time.deltaTime * walkingBobbingSpeedZ;
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, defaultPosZ + Mathf.Sin(timerZ) * bobbingAmountZ);
                }
            }
            else
            {
                //Idle
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);

                if (bobbingZaxis)
                {
                    timerZ = 0;
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, defaultPosZ, Time.deltaTime * walkingBobbingSpeedZ));
                }
            }
        }
        else
        {
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);

            if (bobbingZaxis)
            {
                timerZ += Time.deltaTime * walkingBobbingSpeedZ;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, defaultPosZ + Mathf.Sin(timerZ) * bobbingAmountZ);
            }
        }
    }
}

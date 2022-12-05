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

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
        defaultPosY = transform.localPosition.y;
        defaultPosZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkingBobbingSpeed *= 1.5f;
            bobbingAmount *= 1.5f;
            walkingBobbingSpeedZ *= 1.5f;
            bobbingAmountZ *= 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkingBobbingSpeed /= 1.5f;
            bobbingAmount /= 1.5f;
            walkingBobbingSpeedZ /= 1.5f;
            bobbingAmountZ /= 1.5f;
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
}

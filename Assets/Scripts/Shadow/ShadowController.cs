using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShadowController : MonoBehaviour
{
    [Header("Assigned Properties")]
    public GameObject Player;
    public GameObject ShadowBody;
    public VisualEffect Smoke;

    [Header("Public Variables")]
    public bool LookAtPlayer;
    public float TimeToDestroy;
    public float EmergeDuration;
    public float MovementSpeed;

    [Header("Other Variables")]
    public bool isFading;
    public bool isAlive;
    private bool isChasing;
    private float fadingDelay;
    private string alpha = "AlphaChange";
    private float standardSpeed;

    private void Start()
    {
        standardSpeed = MovementSpeed;
        ResetValues();
    }
    public void Emerge()//Shadow starts walking out of the wall, but doesn't go into chase yet
    {
        ResetValues();
        isChasing = false;
        isAlive = true;
        Debug.Log("I spawn in now");
        StartCoroutine(Emerging());
    }

    private void Update()
    {
        if (Player != null && LookAtPlayer)
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z), Vector3.up);

        #region Fading Out

        if (isFading && isAlive)
        {
            TakeDamage();
        }
        else
        {
            ResetValues();
        }

        #endregion

        #region Chase

        if (isChasing)
        {
            transform.position += transform.forward * Time.deltaTime * MovementSpeed;
        }
        Debug.Log(isChasing);

        #endregion
    }

    IEnumerator Emerging()
    {
        //Move point A to point B
        //begin soundtrack
        //play emerging sound

        yield return new WaitForSeconds(EmergeDuration);

        if (isAlive)
        {
            Debug.Log("Start chase");
            isChasing = true;
        }
    }

    private void TakeDamage()//When shining the red light on the Shadow
    {
        fadingDelay -= Time.deltaTime;
        MovementSpeed = standardSpeed / 2;
        Smoke.SetFloat(alpha, TimeToDestroy - fadingDelay);
        //Play take damage sound

        if(fadingDelay <= 0)
        {
            Die();
        }
    }

    public void Die()//When the Shadow is defeated
    {
        Debug.Log("AM ded");
        //Play death sound
        Smoke.Stop();
        isChasing = false;
        ShadowBody.SetActive(false);
        isAlive = false;
    }

    private void ResetValues()//Resets all local values to prepare for new spawn
    {
        fadingDelay = TimeToDestroy;
        Smoke.SetFloat(alpha, 0);
        MovementSpeed = standardSpeed;
        //Smoke.Play();
        ShadowBody.SetActive(true);
    }

    private void KillPlayer()
    {
        //Kill the player
    }
}

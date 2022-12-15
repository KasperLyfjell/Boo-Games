using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShadowController : MonoBehaviour
{
    private AudioSource Sound;

    [Header("Assigned Properties")]
    public GameObject Player;
    public GameObject ShadowBody;
    public VisualEffect Smoke;

    [Header("Public Variables")]
    public bool LookAtPlayer;
    public bool ShouldSpawn;
    public float TimeToDestroy;
    public float EmergeDuration;
    public float MovementSpeed;
    public ShadowSpawnerManager ActiveSpawner;
    public Light effectLight;

    [Header("Other Variables")]
    [HideInInspector] public bool isFading;
    [HideInInspector] public bool isAlive;
    private bool isChasing;
    private float fadingDelay;
    private string alpha = "AlphaChange";
    private float standardSpeed;
    private bool Walking;
    private Vector3 walkTo;
    private bool beginChase;

    [Header("Audio")]
    public AudioSource DamageAsyncSound;
    public List<AudioClip> AppearVoice;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;

    private void Start()
    {
        Sound = GetComponent<AudioSource>();
        standardSpeed = MovementSpeed;
        ResetValues();
    }
    public void Emerge(Vector3 StartPos, Vector3 EndPos)//Shadow starts walking out of the wall, but doesn't go into chase yet
    {
        ResetValues();
        isChasing = false;
        isAlive = true;
        PlaySound(AppearVoice[Random.Range(0, AppearVoice.Count)]);

        WalkPath(StartPos, EndPos, standardSpeed / 2, true, true);
    }

    private void Update()
    {
        if (Player != null && LookAtPlayer)
        {
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z), Vector3.up);
        }

        #region Fading Out
        //TEST FUNCTION
        if (Input.GetKey(KeyCode.F))
            isFading = true;
        else
            isFading = false;

        if (isAlive)
        {
            if (isFading)
            {
                TakeDamage();
            }
            else
            {
                DamageAsyncSound.volume -= Time.deltaTime * 1.7f;
                MovementSpeed = standardSpeed;

                if(fadingDelay < TimeToDestroy)
                {
                    fadingDelay += Time.deltaTime;
                    Smoke.SetFloat(alpha, TimeToDestroy - fadingDelay);
                }
            }
        }

        #endregion

        #region Chase

        if (isChasing)
        {
            transform.position += transform.forward * Time.deltaTime * MovementSpeed;
        }

        #endregion

        if (Walking)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkTo, MovementSpeed * Time.deltaTime);

            if (transform.position == walkTo)
            {
                if (beginChase)
                {
                    if (isAlive)
                    {
                        StartCoroutine(Emerging());
                    }
                }
                else
                {
                    Smoke.Stop();
                    ShadowBody.SetActive(false);
                    effectLight.gameObject.SetActive(false);
                    Walking = false;
                    Invoke("ResetShadow", 5);
                }
            }
        }
    }

    
    IEnumerator Emerging()
    {
        Walking = false;

        yield return new WaitForSeconds(EmergeDuration);

        if (isAlive)
        {
            isChasing = true;
        }
    }
    

    public void WalkPath(Vector3 StartPos, Vector3 EndPos, float Speed, bool look, bool chase)
    {
        LookAtPlayer = look;
        transform.position = StartPos;
        Vector3 relativePos = EndPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        beginChase = chase;

        MovementSpeed = Speed;
        walkTo = EndPos;

        /*
        Smoke.Play();
        ShadowBody.SetActive(true);
        effectLight.gameObject.SetActive(true);
        */

        Walking = true;
    }

    void ResetShadow()
    {
        transform.position = new Vector3(0, 0, 0);
        Smoke.Play();
        ShadowBody.SetActive(true);
        effectLight.gameObject.SetActive(true);
    }

    private void TakeDamage()//When shining the red light on the Shadow
    {
        fadingDelay -= Time.deltaTime;
        MovementSpeed = standardSpeed / 2;
        Smoke.SetFloat(alpha, TimeToDestroy - fadingDelay);

        if (DamageAsyncSound.volume != 1)
        {
            DamageAsyncSound.volume = 1;
            DamageAsyncSound.Play();
        }

        if(fadingDelay <= 0)
        {
            Die();
        }
    }

    public void Die()//When the Shadow is defeated
    {
        isFading = false;
        isAlive = false;
        PlaySound(DeathSFX);
        Smoke.Stop();
        ShadowBody.SetActive(false);
        isChasing = false;

        if (ShouldSpawn)
        {
            ActiveSpawner.InitiateRoutine();
        }
    }

    private void ResetValues()//Resets all local values to prepare for new spawn
    {
        fadingDelay = TimeToDestroy;
        Smoke.SetFloat(alpha, 0);
        MovementSpeed = standardSpeed;
        Smoke.Play();
        ShadowBody.SetActive(true);
    }

    private void KillPlayer()
    {
        //Kill the player
    }

    private void PlaySound(AudioClip clip)
    {
        Sound.clip = clip;
        Sound.Play();
    }
}

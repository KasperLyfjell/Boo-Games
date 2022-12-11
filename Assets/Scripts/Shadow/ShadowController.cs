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

    [Header("Other Variables")]
    public bool isFading;
    public bool isAlive;
    private bool isChasing;
    private float fadingDelay;
    private string alpha = "AlphaChange";
    private float standardSpeed;

    [Header("Audio")]
    public List<AudioClip> AppearVoice;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;

    private void Start()
    {
        Sound = GetComponent<AudioSource>();
        standardSpeed = MovementSpeed;
        ResetValues();
    }
    public void Emerge()//Shadow starts walking out of the wall, but doesn't go into chase yet
    {
        ResetValues();
        isChasing = false;
        isAlive = true;
        PlaySound(AppearVoice[Random.Range(0, AppearVoice.Count)]);
        StartCoroutine(Emerging());
    }

    private void Update()
    {
        if (Player != null && LookAtPlayer)
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z), Vector3.up);

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
                if (Sound.clip == DamageSFX)
                {
                    Sound.Stop();
                    Sound.clip = null;
                }

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
            isChasing = true;
        }
    }

    private void TakeDamage()//When shining the red light on the Shadow
    {
        fadingDelay -= Time.deltaTime;
        MovementSpeed = standardSpeed / 2;
        Smoke.SetFloat(alpha, TimeToDestroy - fadingDelay);

        if (Sound.clip != DamageSFX)
            PlaySound(DamageSFX);

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

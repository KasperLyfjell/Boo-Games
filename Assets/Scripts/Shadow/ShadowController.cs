using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using SUPERCharacter;
using UnityEngine.UI;

public class ShadowController : MonoBehaviour
{
    private AudioSource Sound;

    [Header("Assigned Properties")]
    public GameObject Player;
    public GameObject ShadowBody;
    public VisualEffect Smoke;
    public LanternWheelController LanternWheel;
    public Light LanternLight;
    public GameObject dim;

    [Header("Public Variables")]
    public bool LookAtPlayer;
    public bool ShouldSpawn;
    public float TimeToDestroy;
    public float EmergeDuration;
    public float MovementSpeed;
    public ShadowSpawnerManager ActiveSpawner;
    public Light effectLight;
    public GameObject ShadowTooltip;

    [Header("Other Variables")]
    [HideInInspector] public bool isFading;
    [HideInInspector] public bool isAlive;
    [HideInInspector] public bool doResetOnWalk;
    [HideInInspector] public bool Immune = false;
    [HideInInspector] public bool isChasing;
    private float fadingDelay;
    private string alpha = "AlphaChange";
    private float standardSpeed;
    private bool Walking;
    private Vector3 walkTo;
    private bool beginChase;
    private Camera cam;
    private bool doDim;

    [Header("Audio")]
    public AudioSource DamageAsyncSound;
    public List<AudioClip> AppearVoice;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;
    public AudioClip KillSound;
    public AudioSource Jumpscare;
    public AudioSource BGM;

    private void Start()
    {
        cam = Camera.main;
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

        WalkPath(StartPos, EndPos, standardSpeed / 2, true, true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            KillPlayer();
        }
    }

    private void Update()
    {
        if (Player != null && LookAtPlayer)
        {
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z), Vector3.up);
        }

        #region Fading Out


        Vector3 viewpos = cam.WorldToViewportPoint(transform.position);

        if (viewpos.x < 0.75f && viewpos.x > 0.25f && viewpos.y < 0.5f && viewpos.y > -0.3f && viewpos.z < 15)
        {
            if (LanternWheel != null)
            {
                if (LanternWheel.lighterEquipped == false && LanternLight.color == Color.red)
                {
                    isFading = true;
                }
            }
        }
        else
            isFading = false;


        if (isAlive && !Immune)
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
            //transform.position += transform.forward * Time.deltaTime * MovementSpeed;
            Vector3 target = new Vector3(Player.transform.position.x, Player.transform.position.y - 2, Player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * MovementSpeed);
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
                    if (doResetOnWalk)
                    {
                        Smoke.Stop();
                        ShadowBody.SetActive(false);
                        effectLight.gameObject.SetActive(false);
                        Invoke("ResetShadow", 5);
                    }

                    Walking = false;
                }
            }
        }

        if (doDim)
        {
            dim.GetComponent<Image>().color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
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
    

    public void WalkPath(Vector3 StartPos, Vector3 EndPos, float Speed, bool look, bool chase, bool reset)
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

        doResetOnWalk = reset;
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
        Debug.Log("I KILL :3");
        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer()
    {
        BGM.Stop();
        isAlive = false;
        isChasing = false;

        Player.GetComponent<SUPERCharacterAIO>().enableCameraControl = false;
        Player.GetComponent<SUPERCharacterAIO>().enableMovementControl = false;
        Player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        Jumpscare.Play();

        transform.parent = cam.gameObject.transform;
        transform.localPosition = new Vector3(0, -4.5f, 1.8f);
        LookAtPlayer = false;
        transform.localRotation = Quaternion.Euler(cam.transform.rotation.x, cam.transform.rotation.y + 180, cam.transform.rotation.z);

        yield return new WaitForSeconds(1);

        PlaySound(KillSound);
        dim.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        dim.SetActive(true);
        doDim = true;

        yield return new WaitForSeconds(3);

        ShadowTooltip.SetActive(true);

        yield return new WaitForSeconds(3);

        ShadowTooltip.SetActive(false);
        transform.parent = null;
        transform.position = new Vector3(0, 0, 0);
        LookAtPlayer = true;
        Die();

        //spawn player at location
        Player.GetComponent<SUPERCharacterAIO>().enableCameraControl = true;
        Player.GetComponent<SUPERCharacterAIO>().enableMovementControl = true;

        doDim = false;
        dim.SetActive(false);
        BGM.Play();
    }

    private void PlaySound(AudioClip clip)
    {
        Sound.clip = clip;
        Sound.Play();
    }
}

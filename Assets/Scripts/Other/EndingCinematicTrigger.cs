using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class EndingCinematicTrigger : MonoBehaviour
{
    public GameObject oldShadow;
    public GameObject NewShadow;
    public GameObject NewLantern;
    public GameManager manager;

    public Animator armAnim;
    public LanternWheelController wheel;

    public SUPERCharacterAIO player;
    private GameObject PlayerObj;
    public Headbob bobbing;
    public Camera cam;
    public Vector3 StartingPosition;
    public Quaternion StartingRotation;

    public List<AudioSource> DeactivateAudios;
    public List<GameObject> DeactiveObjects;

    public PlayableDirector Cinematic;

    public GameObject MansionInsides;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            triggerCinematic();
        }
    }

    void triggerCinematic()
    {
        MansionInsides.SetActive(false);
        manager.FinishedGame = true;
        oldShadow.SetActive(false);
        PlayerObj = player.gameObject;
        PlayerObj.transform.position = StartingPosition;
        PlayerObj.transform.rotation = StartingRotation;
        player.enableCameraControl = false;
        player.enableMovementControl = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        bobbing.OverrideBobbing = true;

        foreach(AudioSource audio in DeactivateAudios)
        {
            audio.Stop();
        }

        foreach (GameObject obj in DeactiveObjects)
        {
            obj.SetActive(false);
        }

        Cinematic.Play();


        //movePlayer = true;
    }

    public void dropLantern()
    {
        NewShadow.SetActive(true);
        armAnim.SetBool("Reload", true);

        Invoke("spawnLantern", 1);
    }

    void spawnLantern()
    {
        //Remember some drop sound
        NewLantern.SetActive(true);
    }

    public void equipLighter()
    {
        wheel.EquipLighter();
    }


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            triggerCinematic();
        }
    }
#endif
}

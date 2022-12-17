using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class EndingCinematicTrigger : MonoBehaviour
{
    public GameObject NewShadow;
    public GameObject NewLantern;

    public SUPERCharacterAIO player;
    private GameObject PlayerObj;
    public Headbob bobbing;
    public Camera cam;
    public Vector3 StartingPosition;
    public Quaternion StartingRotation;

    public List<AudioSource> DeactivateAudios;

    public PlayableDirector Cinematic;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            triggerCinematic();
        }
    }

    void triggerCinematic()
    {
        PlayerObj = player.gameObject;
        PlayerObj.transform.position = StartingPosition;
        PlayerObj.transform.rotation = StartingRotation;
        player.enableCameraControl = false;
        player.enableMovementControl = false;

        bobbing.OverrideBobbing = true;

        foreach(AudioSource audio in DeactivateAudios)
        {
            audio.Stop();
        }

        Cinematic.Play();


        //movePlayer = true;
    }

    public void dropLantern()
    {
        NewShadow.SetActive(true);
        //Play reload animation
        //Add on some audio to emphasize the character tripping

        Invoke("spawnLantern", 1);
    }

    void spawnLantern()
    {
        //Remember some drop sound
        NewLantern.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class EndingCinematicTrigger : MonoBehaviour
{
    public MoveShadow shadowTrigger;
    public ShadowController Shadow;

    public SUPERCharacterAIO player;
    private GameObject PlayerObj;
    public Headbob bobbing;
    public Camera cam;
    public Vector3 StartingPosition;
    public Quaternion StartingRotation;
    public GameObject WalkToPos;
    //public Vector3 StartingRotation;

    public List<AudioSource> DeactivateAudios;

    public PlayableDirector Cinematic;

    //public AudioSource ChaseBGM;

    //public List<Vector3> SingleCutAnimationPositions;
    private int MoveTo;

    private bool movePlayer;


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

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            triggerCinematic();
        }
#endif

        
        if (movePlayer)
        {
            /*
            Vector3 target = Vector3.RotateTowards(player.gameObject.transform.position, WalkToPos.transform.position, 1 * Time.deltaTime, 0);
            //player.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(player.gameObject.transform.rotation.x, target.y, player.gameObject.transform.rotation.z));
            player.gameObject.transform.rotation = Quaternion.LookRotation(target);

            player.MovePlayer(Vector3.forward, player.currentGroundSpeed);
            */


            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, WalkToPos.transform.rotation, 2 * Time.deltaTime);
            //PlayerObj.transform.position = Vector3.MoveTowards(PlayerObj.transform.position, SingleCutAnimationPositions[MoveTo], 2 * Time.deltaTime);
        }
        
    }
}

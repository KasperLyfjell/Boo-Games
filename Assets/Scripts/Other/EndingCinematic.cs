using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class EndingCinematic : MonoBehaviour
{
    public SUPERCharacterAIO player;
    public Vector3 StartingPosition;
    public Vector3 StartingRotation;

    public PlayableDirector Cinematic;

    public AudioSource ChaseBGM;
    public AudioSource Voicelines;
    public AudioSource Breathing;

    public List<DynamicAudioZone> Voices;


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCinematic();
        }
    }
#endif

    public void StartCinematic()
    {
        player.gameObject.transform.position = StartingPosition;
        player.gameObject.transform.rotation = Quaternion.Euler(StartingRotation);

        player.enableCameraControl = false;
        player.enableMovementControl = false;

        Voicelines.Play();

        //TEST
        Invoke("EndCinematic", Voicelines.clip.length);
    }

    private void EndCinematic()
    {
        player.enableCameraControl = true;
        player.enableMovementControl = true;
        player.BeginChase(75, 300);

        ChaseBGM.Play();
        Breathing.Play();

        foreach(DynamicAudioZone whisper in Voices)
        {
            whisper.gameObject.SetActive(true);
            whisper.IsInside = true;
        }
    }
}
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
    public Vector3 StartingPosition;
    //public Vector3 StartingRotation;

    public PlayableDirector Cinematic;

    public AudioSource ChaseBGM;
    //public AudioSource Breathing;

    //public List<DynamicAudioZone> Voices;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            triggerCinematic();
        }
    }

    void triggerCinematic()
    {

    }
}

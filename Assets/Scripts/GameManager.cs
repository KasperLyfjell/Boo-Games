using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public bool PlayIntro;

    public GameObject EditorLightUp;
    public PlayableDirector Intro;

    public SUPERCharacterAIO player;

    private void Start()
    {
        EditorLightUp.SetActive(false);

        if (PlayIntro)
            NewGameStart();
        else
            CutsceneEnd();

#if !UNITY_EDITOR //Build options
        NewGameStart();
        player.canSprint = false;
#endif
    }


    public void NewGameStart()
    {
        Intro.Play();
    }
    public void CutsceneEnd()
    {
        Debug.Log("you can walk now");
        player.enableCameraControl = true;
        player.enableMovementControl = true;
    }
}

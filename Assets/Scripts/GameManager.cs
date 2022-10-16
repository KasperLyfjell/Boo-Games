using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public LanternWheelController Wheel;

    public bool PlayIntro;

    public GameObject EditorLightUp;
    public PlayableDirector Intro;

    public SUPERCharacterAIO player;

    public Animator Lighter;

    public Light LanternLight;

    private void Start()
    {
        EditorLightUp.SetActive(false);

#if UNITY_EDITOR
        if (PlayIntro)
            NewGameStart();
        else
            CutsceneEnd();
#endif

#if !UNITY_EDITOR //Build options
        NewGameStart();
        player.canSprint = false;
#endif
    }


    public void NewGameStart()
    {
        Intro.Play();
    }
    public void HideLighter()
    {
        //Lighter.SetBool("Equip", false);
        Lighter.gameObject.SetActive(false);
    }

    public void ShowLighter()
    {
        Wheel.EquipLighter();
        /*
        Lighter.gameObject.SetActive(true);
        Lighter.SetBool("Equip", true);
        */
    }

    public void CutsceneEnd()
    {
        Debug.Log("you can walk now");
        player.enableCameraControl = true;
        player.enableMovementControl = true;

        if(Lighter.gameObject.activeSelf == false)
        {
            ShowLighter();
        }
    }
}

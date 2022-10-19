using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LanternWheelController Wheel;

    public bool PlayIntro;
    public bool PreviewIngameSprint;

    public GameObject EditorLightUp;
    public PlayableDirector Intro;

    public SUPERCharacterAIO player;

    public Animator Lighter;
    public Animator Photograph;

    public Light LanternLight;

    public GameObject MenuCanvas;


    private bool MenuOpen;
    private KeyCode MenuOpenButton;

    private bool playingCutscene;

    private void Start()
    {
        EditorLightUp.SetActive(false);
        playingCutscene = true;

#if UNITY_EDITOR
        if (PlayIntro)
            NewGameStart();
        else
            CutsceneEnd();

        MenuOpenButton = KeyCode.Tab;

        if(!PreviewIngameSprint)
            player.sprintingSpeed = 600;
#endif

#if !UNITY_EDITOR //Build options
        NewGameStart();
        player.canSprint = false;

        MenuOpenButton = KeyCode.Escape;
#endif
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(MenuOpenButton) && !playingCutscene)
        {
            if (MenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
        */
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
        playingCutscene = false;
        player.enableCameraControl = true;
        player.enableMovementControl = true;

        if(Lighter.gameObject.activeSelf == false)
        {
            ShowLighter();
        }
    }

    public void ShowPhotograph()
    {
        Photograph.SetBool("Equipped", true);
    }

    public void HidePhotograph()
    {
        Photograph.SetBool("Equipped", false);
    }

    public void OpenMenu()
    {
        MenuOpen = true;

        player.enableMovementControl = false;
        player.enableCameraControl = false;
        Cursor.lockState = CursorLockMode.None;
        //player.lockAndHideMouse = false;
        MenuCanvas.SetActive(true);
    }

    public void CloseMenu()
    {
        MenuOpen = false;

        player.enableMovementControl = true;
        player.enableCameraControl = true;
        Cursor.lockState = CursorLockMode.Locked;
        //player.lockAndHideMouse = true;
        MenuCanvas.SetActive(false);
    }

    public void MainMenuBack()
    {
        SceneManager.LoadScene(0);
    }
}
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

    public PlayableDirector Intro;

    public SUPERCharacterAIO player;

    public Animator Lighter;
    public Animator Photograph;

    public Light LanternLight;

    public GameObject MenuCanvas;
    public List<GameObject> CanvasItems = new List<GameObject>();

    public GameObject MansionInside;


    private bool MenuOpen;
    private KeyCode MenuOpenButton;

    private bool playingCutscene;

    private void Start()
    {
        MansionInside.SetActive(false);
        playingCutscene = true;
        //Lighter.gameObject.SetActive(false);

#if UNITY_EDITOR
        if (PlayIntro)
            NewGameStart();
        else
        {
            CutsceneEnd();
            StartCoroutine(EditorGameStart());
        }

        MenuOpenButton = KeyCode.Tab;

        if(!PreviewIngameSprint)
            player.sprintingSpeed = 600;
#endif

#if !UNITY_EDITOR //Build options
        NewGameStart();
        //player.canSprint = false;

        MenuOpenButton = KeyCode.Escape;
#endif
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(MenuOpenButton) && !playingCutscene)
        {
            if (MenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
        
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
        Cursor.visible = true;
        //player.lockAndHideMouse = false;
        MenuCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        MenuOpen = false;

        player.enableMovementControl = true;
        player.enableCameraControl = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //player.lockAndHideMouse = true;
        MenuCanvas.SetActive(false);
        foreach(GameObject item in CanvasItems)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void MainMenuBack()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Test_MainMenu");
    }


    IEnumerator EditorGameStart()
    {
        yield return new WaitForSeconds(1);

        ShowLighter();
    }
}
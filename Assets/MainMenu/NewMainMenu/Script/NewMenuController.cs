using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using echo17.EndlessBook;
using TMPro;
using UnityEngine.Audio;

public class NewMenuController : MonoBehaviour
{
    [Header("Endless Book")]
    public EndlessBook book;
    public Animator pageOne;
    public Animator pageTwo;

    [Header("Levels To Load")]
    public string _newGameLevel;
    public string _testScene;
    private string levelToLoad;

    [Header("MainMenuFolders")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject soundOptions;

    public float delayValue = 2;

    [Header("Master Volume")]
    public TMP_Text masterVolumeTextValue = null;
    public Slider masterVolumeSilder = null;
    //public float defaultVolume = 100f;

    [Header("Music Volume")]
    public AudioMixer musicMixer;
    public TMP_Text musicVolumeTextValue = null;
    public Slider musicVolumeSlider = null;
    //public float musicDefaultVolume = 100f;
    
    [Header("Ambience Volume")]
    public AudioMixer ambienceMixer;
    public TMP_Text ambienceVolumeTextValue = null;
    public Slider ambienceVolumeSlider = null;
    
    [Header("SFX Volume")]
    public AudioMixer sFXMixer;
    public TMP_Text sFXVolumeTextValue = null;
    public Slider sFXVolumeSlider = null;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pageOne.Play("TurnForward");
        StartCoroutine(DelayStart(delayValue));
    }

    IEnumerator DelayStart(float delayTime)
    {
        yield return new WaitForSeconds(delayValue);

        //BlackScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void Update()
    {
        masterVolumeTextValue.text = masterVolumeSilder.value.ToString("0");
        musicVolumeTextValue.text = musicVolumeSlider.value.ToString("0");
        ambienceVolumeTextValue.text = ambienceVolumeSlider.value.ToString("0");
        sFXVolumeTextValue.text = sFXVolumeSlider.value.ToString("0");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LaunchTestScene()
    {
        SceneManager.LoadScene(_testScene);
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void MenuOptions()
    {
        mainMenu.SetActive(false);

        book.SetPageNumber(3);
        pageTwo.Play("TurnForward");

        StartCoroutine(DelayOption(delayValue));
    }

    IEnumerator DelayOption(float delayTime)
    {
        yield return new WaitForSeconds(delayValue);

        options.SetActive(true);
        soundOptions.SetActive(true);
    }

    public void MenuMain()
    {
        options.SetActive(false);
        soundOptions.SetActive(false);

        book.SetPageNumber(2);
        pageTwo.Play("TurnBackward");

        StartCoroutine(DelayMainMenu(delayValue));
    }

    IEnumerator DelayMainMenu(float delayTime)
    {
        yield return new WaitForSeconds(delayValue);

        mainMenu.SetActive(true);
    }

    public void MasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void MusicVolume(float musicLvl)
    {
        musicMixer.SetFloat("MasterMusic", musicLvl);
    }

    public void AmbienceVolume(float ambienceLvl)
    {
        ambienceMixer.SetFloat("MasterAmbience", ambienceLvl);
    }

    public void SFXVolume(float sFXLvl)
    {
        sFXMixer.SetFloat("MasterSFX", sFXLvl);
    }



    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }
}
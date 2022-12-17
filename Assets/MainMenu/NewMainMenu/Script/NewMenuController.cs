using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using echo17.EndlessBook;
using TMPro;
using UnityEngine.Audio;
using HFPS.UI;

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
    public GameObject blackout;

    [Header("MainMenuFolders")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject soundOptions;
    public GameObject graphicsOptions;

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

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions; 

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        blackout.SetActive(false);
        pageOne.Play("TurnForward");
        StartCoroutine(DelayStart(delayValue));

        //Resolution Settings
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == resolutions[i].height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
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
        musicVolumeTextValue.text = Mathf.Round(musicVolumeSlider.value * 100).ToString("0");
        ambienceVolumeTextValue.text = Mathf.Round(ambienceVolumeSlider.value * 100).ToString("0");
        sFXVolumeTextValue.text = Mathf.Round(sFXVolumeSlider.value * 100).ToString("0");
    }

    public void StartGame()
    {
        blackout.SetActive(true);
        StartCoroutine(DelayNewScene(1));
    }

    IEnumerator DelayNewScene(float delaytime)
    {
        yield return new WaitForSeconds(delayValue);

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
        graphicsOptions.SetActive(false);

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

    public void MusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MasterMusic", Mathf.Log10(sliderValue) * 20);
    }

    public void AmbienceVolume(float sliderValue)
    {
        ambienceMixer.SetFloat("MasterAmbience", Mathf.Log10(sliderValue) * 20);
    }

    public void SFXVolume(float sliderValue)
    {
        sFXMixer.SetFloat("MasterSFX", Mathf.Log10(sliderValue) * 20);
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
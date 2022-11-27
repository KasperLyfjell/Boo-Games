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
    private string levelToLoad;

    [Header("MainMenuFolders")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject soundOptions;

    public float delayValue = 2;

    [Header("Volume Setting")]
    public TMP_Text masterVolumeTextValue = null;
    public Slider masterVolumeSilder = null;
    public float defaultVolume = 100f;

    [Header("Volume Masters")]
    public AudioMixer ambienceMixer;
    public AudioMixer musicMixer;
    public AudioMixer sFXMixer;
    private void Start()
    {
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
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_newGameLevel);
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

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }



    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using echo17.EndlessBook;

public class NewMenuController : MonoBehaviour
{
    [Header("Endless Book")]
    public EndlessBook book;
    public Animator pages;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    [Header("MainMenuFolders")]
    public GameObject mainMenu;
    public GameObject Options;

    public float delayValue = 2;

    public void NewGameDialog()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void MenuOptions()
    {
        mainMenu.SetActive(false);

        book.SetPageNumber(3);
        pages.Play("TurnForward");

        StartCoroutine(DelayOption(delayValue));
    }

    IEnumerator DelayOption(float delayTime)
    {
        yield return new WaitForSeconds(delayValue);

        Options.SetActive(true);
    }

    public void MenuMain()
    {
        Options.SetActive(false);

        book.SetPageNumber(2);
        pages.Play("TurnBackward");

        StartCoroutine(DelayMainMenu(delayValue));
    }

    IEnumerator DelayMainMenu(float delayTime)
    {
        yield return new WaitForSeconds(delayValue);

        mainMenu.SetActive(true);
    }
}
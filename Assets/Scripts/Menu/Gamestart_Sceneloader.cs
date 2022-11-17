using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamestart_Sceneloader : MonoBehaviour
{
    public GameObject steamManager;

    private void Start()
    {
        DontDestroyOnLoad(steamManager);
        SceneManager.LoadScene("MainMenu");
    }
}

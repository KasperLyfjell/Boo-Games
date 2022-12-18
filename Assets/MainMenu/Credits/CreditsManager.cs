using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    private float delayValue = 60;

    private void Start()
    {
        StartCoroutine(SceneChangeDelay(delayValue));
    }

    IEnumerator SceneChangeDelay(float delaytime)
    {
        yield return new WaitForSeconds(delayValue);
        SceneManager.LoadScene(0);
    }
}

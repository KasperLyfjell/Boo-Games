using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class GameManager : MonoBehaviour
{
    public GameObject EditorLightUp;

    public SUPERCharacterAIO player;

    private void Start()
    {
        EditorLightUp.SetActive(false);
    }

    public void CutsceneEnd()
    {
        Debug.Log("you can walk now");
        player.enableCameraControl = true;
        player.enableMovementControl = true;
    }
}

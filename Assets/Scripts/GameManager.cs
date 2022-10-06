using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class GameManager : MonoBehaviour
{
    public SUPERCharacterAIO player;

    public void CutsceneEnd()
    {
        Debug.Log("you can walk now");
        player.enableCameraControl = true;
        player.enableMovementControl = true;
    }
}

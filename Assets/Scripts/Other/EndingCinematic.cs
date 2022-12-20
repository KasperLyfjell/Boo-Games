using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using UnityEngine.Playables;

public class EndingCinematic : MonoBehaviour
{
    public MoveShadow shadowTrigger;
    public ShadowController Shadow;
    public Light ShadowLight;
    private Color shadowRed = new Color(0.8962264f, 0.1648719f, 0.1648719f, 1);

    public SUPERCharacterAIO player;
    public Vector3 StartingPosition;
    public Vector3 StartingRotation;

    public PlayableDirector Cinematic;

    public AudioSource ChaseBGM;
    public AudioSource Breathing;

    public List<DynamicAudioZone> Voices;

    public GameObject EndingTriggers;

    public List<GameObject> EditorCheats;
    public GameObject InvisWallToBasement;
    public GameObject OutsideTerrain;

    public GameObject playerSpawnPoint;


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach(GameObject item in EditorCheats)
            {
                item.SetActive(!item.activeSelf);
            }

            StartCinematic();
        }
    }
#endif

    public void StartCinematic()
    {
        InvisWallToBasement.SetActive(true);
        OutsideTerrain.SetActive(true);
        Shadow.ResetShadow();
        player.gameObject.transform.position = StartingPosition;
        player.gameObject.transform.rotation = Quaternion.Euler(StartingRotation);

        player.enableCameraControl = false;
        player.enableMovementControl = false;

        Shadow.AssingPlayerRespawn(playerSpawnPoint);
        Shadow.Immune = true;
        shadowTrigger.TriggerEvent();
        ShadowLight.color = shadowRed;

        EndingTriggers.SetActive(true);

        Cinematic.Play();
    }

    public void EndCinematic()
    {
        player.enableCameraControl = true;
        player.enableMovementControl = true;
        player.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 192.6449f, 0));
        player.BeginChase(75, 300);

        ChaseBGM.Play();
        Breathing.Play();

        Shadow.ActiveSpawner = gameObject.GetComponent<ShadowSpawnerManager>();
        Shadow.ShouldSpawn = true;
        Shadow.isChasing = true;
        Shadow.AssingBGM(ChaseBGM);
        Shadow.ChangeTooltip("Run, Flee, Escape");

        foreach (DynamicAudioZone whisper in Voices)
        {
            whisper.gameObject.SetActive(true);
            whisper.IsInside = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject SpecialLight;
    public ShadowController Shadow;

    public AudioSource playAudio;

    public MoveShadow ShadowTrigger;

    public Env_Door BedroomDoor;
    public Env_Door KidsDoor;

    private bool triggered;
    private Vector3 shadowStart = new Vector3(-7.86999989f, 74.8000031f, 450.589996f);
    private Vector3 shadowEnd = new Vector3(0.939999998f, 74.8000031f, 447.220001f);

    public GameObject endingtrigger;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //SpecialLight.SetActive(true);
            player.transform.position = new Vector3(66.4199982f, 73.7699966f, 428.230011f);
        }
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !triggered)
        {
            StartCoroutine(HallwayEvent());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            KidsDoor.ShutDoor();
            //Shadow.transform.position = new Vector3(1.39999998f, 75.0699997f, 447.230011f);
            //spawnShadow();
        }
    }

    public void spawnShadow()
    {
        ShadowTrigger.TriggerEvent();
    }

    IEnumerator HallwayEvent()
    {
        triggered = true;
        endingtrigger.SetActive(true);
        playAudio.Play();
        KidsDoor.ShutDoor();

        yield return new WaitForSeconds(2);

        SpecialLight.SetActive(true);
        BedroomDoor.MoveOpenDoor(100);

        yield return new WaitForSeconds(5);

        Shadow.Emerge(shadowStart, shadowEnd);
    }
}

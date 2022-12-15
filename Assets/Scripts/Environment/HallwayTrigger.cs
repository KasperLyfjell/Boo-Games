using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject SpecialLight;
    public GameObject Shadow;


    public MoveShadow ShadowTrigger;

    public Env_Door BedroomDoor;
    public Env_Door KidsDoor;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpecialLight.SetActive(true);
            player.transform.position = new Vector3(66.4199982f, 73.7699966f, 428.230011f);
        }
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            BedroomDoor.MoveOpenDoor(60);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            KidsDoor.ShutDoor();
            Shadow.transform.position = new Vector3(1.39999998f, 75.0699997f, 447.230011f);
            //spawnShadow();
        }
    }

    public void spawnShadow()
    {
        ShadowTrigger.TriggerEvent();
    }
}

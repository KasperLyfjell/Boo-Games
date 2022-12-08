using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public GameObject Player;


    public void Emerge()
    {
        Debug.Log("I spawn in now");
    }

    private void Update()
    {
        if (Player != null)
            transform.LookAt(Player.transform.position, Vector3.up);
        else
            Debug.LogError("Player not assigned to shadow script");
    }
}

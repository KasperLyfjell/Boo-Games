using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public GameObject Player;

    [Header("Variables")]
    public bool LookAtPlayer;

    public void Emerge()
    {
        Debug.Log("I spawn in now");
    }

    private void Update()
    {
        if (Player != null && LookAtPlayer)
            transform.LookAt(Player.transform.position, Vector3.up);
    }
}

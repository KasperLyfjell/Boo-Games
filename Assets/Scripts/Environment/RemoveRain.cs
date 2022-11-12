using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRain : MonoBehaviour
{
    public GameObject rain;

    private void OnTriggerEnter(Collider other)
    {
        rain.SetActive(false);
        Destroy(this.gameObject);
    }
}

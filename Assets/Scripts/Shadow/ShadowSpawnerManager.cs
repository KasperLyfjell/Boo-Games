using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawnerManager : MonoBehaviour
{
    [Tooltip("The shadow in the scene")]
    public GameObject Shadow;
    private ShadowController csShadow;

    [Header("Spawning Details")]
    [Tooltip("Spawners from which the shadow will spawn")]
    public List<GameObject> Spawners = new List<GameObject>();
    public float minDelay;
    public float maxDelay;

    private float currentDelay;
    private GameObject currentSpawner;
    private bool doSpawn;

    private void Start()
    {
        csShadow = Shadow.GetComponent<ShadowController>();
        InitiateRoutine();
    }

    public void SpawnShadow()
    {

    }

    public void InitiateRoutine()
    {
        doSpawn = true;
        currentSpawner = Spawners[Random.Range(0, Spawners.Count)];
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        int randomSpawn = Random.Range(0, Spawners.Count);
        while(Spawners[randomSpawn] == currentSpawner)//Make sure to not spawn the same place twice, just for feel
        {
            randomSpawn = Random.Range(0, Spawners.Count);
        }
        currentSpawner = Spawners[randomSpawn];

        csShadow.Emerge();

        Shadow.transform.position = currentSpawner.transform.position;
        currentDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(currentDelay);

        if (doSpawn)
            StartCoroutine(SpawnRoutine());
    }

    public void EndSpawn()
    {
        doSpawn = false;
    }
}

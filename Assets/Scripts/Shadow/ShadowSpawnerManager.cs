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
    private Transform WalkTo;
    public bool doSpawn;

    private void Start()
    {
        csShadow = Shadow.GetComponent<ShadowController>();

        if(doSpawn)
            SpawnShadow(null);
    }

    public void SpawnShadow(GameObject SpawnPoint)//Instantly spawns shadow at location without waiting
    {
        if(SpawnPoint == null)
        {
            int randomSpawn = Random.Range(0, Spawners.Count);
            while (Spawners[randomSpawn] == currentSpawner)//Make sure to not spawn the same place twice, just for feel
            {
                randomSpawn = Random.Range(0, Spawners.Count);
            }
            currentSpawner = Spawners[randomSpawn];
        }
        else
        {
            currentSpawner = SpawnPoint;
        }

        //Shadow.transform.position = currentSpawner.transform.position;
        WalkTo = currentSpawner.transform.GetChild(0);

        csShadow.Emerge(currentSpawner.transform.position, WalkTo.position);
    }

    public void InitiateRoutine()
    {
        //doSpawn = true;
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
        WalkTo = currentSpawner.transform.GetChild(0);

        currentDelay = Random.Range(minDelay, maxDelay);

        yield return new WaitForSeconds(currentDelay);


        csShadow.Emerge(currentSpawner.transform.position, WalkTo.position);

        //Shadow.transform.position = currentSpawner.transform.position;
        
        /*
        if (doSpawn)
            StartCoroutine(SpawnRoutine());   
        */
    }

    public void EndSpawn()
    {
        doSpawn = false;
    }
}

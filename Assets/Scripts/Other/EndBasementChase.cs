using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBasementChase : MonoBehaviour
{
    public AudioSource BGM;
    public ShadowController shadow;
    public ShadowSpawnerManager spawner;
    private bool exited;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !exited)
        {
            shadow.ShouldSpawn = false;
            shadow.Die();
            Destroy(spawner);
            exited = true;
        }
    }

    private void Update()
    {
        if (exited)
        {
            if (BGM.volume != 0)
                BGM.volume -= Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}

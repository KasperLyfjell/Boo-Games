using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNeedingKey : MonoBehaviour
{
    public bool haveKey;
    bool isClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClose && haveKey && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isClose = false;
    }
}

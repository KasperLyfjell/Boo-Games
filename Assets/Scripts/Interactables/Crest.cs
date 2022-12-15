using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crest : MonoBehaviour
{
    public GameObject crestIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        crestIcon.SetActive(true);
        Destroy(gameObject);
    }
}

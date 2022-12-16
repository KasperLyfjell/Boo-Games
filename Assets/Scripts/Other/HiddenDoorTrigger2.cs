using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoorTrigger2 : MonoBehaviour
{
    public Animator cabAnim;
    public AudioSource au;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        cabAnim.SetBool("Open", false);
        au.Play();
    }
}

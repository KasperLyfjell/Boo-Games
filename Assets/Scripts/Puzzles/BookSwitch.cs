using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSwitch : MonoBehaviour
{
    public GameObject shelf;
    Animator shelfAnim;
    AudioSource shelfAudio;

    Animator anim;
    AudioSource au;
    bool played;

    public GameObject hiddenDoor;

    // Start is called before the first frame update
    void Start()
    {
        shelfAnim = shelf.GetComponent<Animator>();
        shelfAudio = shelf.GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();

        hiddenDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (played == false)
        {
            played = true;
            anim.SetBool("Move", true);
            au.Play();
            Invoke("MoveShelf", 1f);
        }
    }

    void MoveShelf()
    {
        hiddenDoor.SetActive(true);
        shelfAnim.SetBool("Move", true);
        shelfAudio.Play();
    }
}

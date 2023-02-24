using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomPuzzle : MonoBehaviour
{
    Animator anim;
    //public AudioClip audioClip;
    AudioSource au;

    public GameObject topPaint;
    public GameObject leftPaint;
    public GameObject crest;

    RotatePainting topP;
    RotatePainting leftP;

    bool played;

    // Start is called before the first frame update
    void Start()
    {
        topP = topPaint.GetComponent<RotatePainting>();
        leftP = leftPaint.GetComponent<RotatePainting>();

        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();

        crest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(played == false)
        {
            if(topP.correct == true && leftP.correct == true)
            {
                played = true;
                crest.SetActive(true);
                Open();
                topP.enabled = false;
                leftP.enabled = false;
            }
        }
    }

    void Open()
    {
        anim.SetBool("Interact", true);
        au.Play();
        //au.PlayOneShot(audioClip);
    }
}

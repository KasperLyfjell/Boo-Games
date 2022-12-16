using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsroomPuzzle : MonoBehaviour
{
    Animator anim;
    //public AudioClip audioClip;
    AudioSource au;

    public GameObject leftPaint;
    public GameObject midPaint;
    public GameObject rightPaint;

    RotatePainting leftP;
    RotatePainting midP;
    RotatePainting rightP;

    bool played;

    // Start is called before the first frame update
    void Start()
    {
        leftP = leftPaint.GetComponent<RotatePainting>();
        midP = midPaint.GetComponent<RotatePainting>();
        rightP = rightPaint.GetComponent<RotatePainting>();

        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (played == false)
        {
            if (leftP.correct == true && midP.correct == false && rightP.correct == true)
            {
                played = true;
                Invoke("RevealDoor", 1f);
            }
        }
    }

    void RevealDoor()
    {
        anim.SetBool("Move", true);
        au.Play();
        //au.PlayOneShot(audioClip);
    }
}

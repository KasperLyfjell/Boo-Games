using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShadow : MonoBehaviour
{
    public ShadowController shadow;

    public Vector3 start;
    public Vector3 end;
    public float speed;
    public bool tolook;
    public bool reset = true;

    bool hasTriggered;

    public AudioTrigger PlayAudioAfter;
    public float playAfter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !hasTriggered)
        {
            TriggerEvent();

            hasTriggered = true;
        }
    }

    public void TriggerEvent()
    {
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();
        shadow.WalkPath(start, end, speed, tolook, false, reset);

        if (PlayAudioAfter)
        {
            Invoke("PlayNewAudio", playAfter);
        }
    }

    void PlayNewAudio()
    {
        PlayAudioAfter.PlaySound();
    }
}

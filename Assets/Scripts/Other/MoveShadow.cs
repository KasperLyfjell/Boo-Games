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

    bool hasTriggered;

    public AudioTrigger PlayAudioAfter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !hasTriggered)
        {
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();
            shadow.WalkPath(start, end, speed, tolook);

            if (PlayAudioAfter)
            {
                Invoke("PlayNewAudio", 1f);
            }

            hasTriggered = true;
        }
    }

    void PlayNewAudio()
    {

    }
}

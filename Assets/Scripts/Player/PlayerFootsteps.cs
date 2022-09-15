using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    CharacterController controller;
    public AudioSource footstepSound;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.velocity.magnitude > 2f && footstepSound.isPlaying == false)
        {
            footstepSound.volume = Random.Range(0.05f, 0.1f);
            footstepSound.pitch = Random.Range(1.3f, 1.5f);
            footstepSound.Play();
        }
    }
}

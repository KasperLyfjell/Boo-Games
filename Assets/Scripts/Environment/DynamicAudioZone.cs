using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicAudioZone : MonoBehaviour
{
    #region Settings
    [Header("Audio Settings")]
    [Tooltip("False: 1 audio source, randomly playing around the player.    True: Multiple, pre-defined audiosources located in the scene")]
    public bool PreDefinedSource;

    [Tooltip("Minimum duration between playing a sound")]
    [SerializeField] private float minDelay;
    [Tooltip("Maximum duration between playing a sound")]
    [SerializeField] private float maxDelay;

    public bool RandomVolume;
    [SerializeField] private float minVolume;
    [SerializeField] private float maxVolume;
    public bool RandomPitch;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    [SerializeField] private AudioSource Source;
    [SerializeField] private List<AudioClip> Clips = new List<AudioClip>(); 

    [SerializeField] private List<AudioSource> AudioSources = new List<AudioSource>();
    #endregion

    #region Local Variables
    private GameObject player;
    private bool IsInside;
    private float delay = 0;
    #endregion

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            if (player == null)
                player = collider.gameObject;

            if (delay == 0)
                delay = Random.Range(minDelay, maxDelay);
            IsInside = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            IsInside = false;
        }
    }


    private void Update()
    {
        if (IsInside)
        {
            delay -= Time.deltaTime;
            Debug.Log(delay);

            if(delay < 0)
            {
                if (PreDefinedSource)//This is used when there are one or multiple, pre-defined source locations which are played procedurally
                {
                    //propably most useful for the house
                }
                else//This will play the sound in a random direction somewhere around the player. Used for playing sounds at random
                {
                    Source.transform.position = player.transform.position;
                    Vector3 offset = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
                    Source.transform.position += offset;

                    //Source.panStereo = Random.Range(-1f, 1f);
                    AudioClip RandomClip = Clips[Random.Range(0, Clips.Count)];

                    PlaySound(Source, RandomClip);
                }
            }
        }
    }

    private void PlaySound(AudioSource source, AudioClip audioclip)
    {
        if (RandomVolume)
            source.volume = Random.Range(minVolume, maxVolume);

        if (RandomPitch)
            source.pitch = Random.Range(minPitch, maxPitch);

        source.clip = audioclip;
        source.Play();

        delay = Random.Range(minDelay, maxDelay);
    }
}

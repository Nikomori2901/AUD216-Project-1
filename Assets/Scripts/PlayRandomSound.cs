using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{

    public List<AudioClip> myAudioClips;

    AudioSource myAudioSource;
    public float minPitch = .95f;
    public float maxPitch = 1.1f;
    public float minVol = 0.6f;
    public float maxVol = 1f;
    public float minDelay = 0.5f;
    public float maxDelay = 4f;


    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!myAudioSource.isPlaying)
        {
            int index = Random.Range(0, myAudioClips.Count);
            myAudioSource.clip = myAudioClips[index];
            myAudioSource.pitch = Random.Range(minPitch, maxPitch);
            myAudioSource.volume = Random.Range(minVol, maxVol);
            myAudioSource.PlayDelayed(Random.Range(minDelay, maxDelay));
            myAudioSource.Play();
        }
    }
}
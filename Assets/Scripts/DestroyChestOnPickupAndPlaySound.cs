using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChestOnPickupAndPlaySound : MonoBehaviour
{
    public GameObject chest;
    public GameObject endScreen;
    public AudioSource myAudioSource;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endScreen.SetActive(true);
            chest.SetActive(false);

            if (myAudioSource != null && myAudioSource.clip != null)
            {
                myAudioSource.Play();
            }
        }
    }
}

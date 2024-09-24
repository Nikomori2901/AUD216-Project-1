using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPickupAndPlaySound : MonoBehaviour
{

    public AudioSource myAudioSource;
    public MeshRenderer meshRenderer;
    public BoxCollider boxCollider;


    public void OnTriggerEnter(Collider other)
    {
        if (myAudioSource.clip != null)
        {
            myAudioSource.Play();
        }
        meshRenderer.enabled=false;
        boxCollider.enabled = false;
    }
}
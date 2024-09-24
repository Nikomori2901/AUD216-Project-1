using UnityEngine;

public class DestroyOnPickupAndPlaySound : MonoBehaviour
{
    private AudioSource myAudioSource;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myAudioSource != null && myAudioSource.clip != null)
        {
            myAudioSource.Play();
        }

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }
}
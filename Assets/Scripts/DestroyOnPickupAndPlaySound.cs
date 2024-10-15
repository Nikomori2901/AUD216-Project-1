using System.Collections;
using UnityEngine;

public class DestroyOnPickupAndPlaySound : MonoBehaviour
{
    private AudioSource myAudioSource;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    public AudioSource ambiance;

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

        StartCoroutine(DestroyPickup());
    }

    private IEnumerator DestroyPickup()
    {
        float volume = ambiance.volume;
        while (volume > 0)
        {
            yield return new WaitForSeconds(0.25f);
            volume -= 0.1f;
            ambiance.volume = volume;
            Debug.Log("Volume: " + ambiance.volume);
        }
        
        Destroy(gameObject);
    }
}
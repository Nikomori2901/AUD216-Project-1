using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CaveEntrance : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip doorStart;
    [SerializeField] AudioClip doorSliding;
    [SerializeField] AudioClip doorOpen;

    [SerializeField] Vector3 closedPosition;

    [SerializeField] float moveSpeed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [Button]
    public void StartOpening()
    {
        Debug.Log("Start Moving");
        StartCoroutine(Open());
        audioSource.clip = doorStart;
        audioSource.Play();
    }
    private IEnumerator Open()
    {
        yield return new WaitForSeconds(2);
        audioSource.clip = doorSliding;
        audioSource.loop = true;
        audioSource.Play();

        while (transform.position != closedPosition)
        {
            Debug.Log("Moving");
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        FinishOpening();
    }

    private void FinishOpening()
    {
        audioSource.clip = doorOpen;
        audioSource.loop = false;
        audioSource.Play();
    }
}

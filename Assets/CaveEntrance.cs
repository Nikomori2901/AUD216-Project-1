using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CaveEntrance : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip doorSliding;
    AudioClip doorOpen;


    private void Start()
    {
        GetComponent<AudioSource>();
    }

    [Button]
    private void StartOpening()
    {
        StartCoroutine(Open());
        // start sliding door down and start looping slide noise
    }
    private IEnumerator Open()
    {
        // slide door down
        yield return new WaitForEndOfFrame();
    }

    private void FinishOpening()
    {
        // play door slam sound
        // stop sliding noise
    }
}

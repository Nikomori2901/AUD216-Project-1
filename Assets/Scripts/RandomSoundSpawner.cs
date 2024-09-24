using UnityEngine;
using System.Collections;

public class RandomSoundSpawner : MonoBehaviour
{
    [Tooltip("Maximum distance on the X-axis that the sound can spawn from the initial position")]
    [SerializeField] private float maxDistanceX = 10f;

    [Tooltip("Maximum distance on the Z-axis that the sound can spawn from the initial position")]
    [SerializeField] private float maxDistanceZ = 10f;

    [Tooltip("Maximum elevation on the Y-axis that the sound can spawn above the initial position")]
    [SerializeField] private float maxElevationY = 5f;

    [Tooltip("Minimum time interval between sound spawns")]
    [SerializeField] private float minTimeInterval = 5f;

    [Tooltip("Maximum time interval between sound spawns")]
    [SerializeField] private float maxTimeInterval = 15f;

    [Tooltip("Array of audio clips to be played randomly")]
    [SerializeField] private AudioClip[] audioClips;

    [Tooltip("If true, waits for the full clip to play before starting the next interval")]
    [SerializeField] private bool waitForFullClipPlay = true;

    [Tooltip("Maximum pitch variation applied to each sound (0 = no variation, 1 = maximum variation)")]
    [SerializeField] private float audioPitchVariation = 0.2f;

    private AudioSource audioSource;
    private Vector3 initialPosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(PlaySoundRoutine());
    }

    private IEnumerator PlaySoundRoutine()
    {
        while (true)
        {
            RepositionAndPlaySound();
            if (waitForFullClipPlay && audioSource.clip != null)
            {
                yield return new WaitForSeconds(audioSource.clip.length);
            }
            yield return new WaitForSeconds(Random.Range(minTimeInterval, maxTimeInterval));
        }
    }

    private void RepositionAndPlaySound()
    {
        // Randomly reposition
        float randomX = Random.Range(-maxDistanceX, maxDistanceX);
        float randomZ = Random.Range(-maxDistanceZ, maxDistanceZ);
        float randomY = Random.Range(0, maxElevationY);
        transform.position = initialPosition + new Vector3(randomX, randomY, randomZ);

        // Play random sound with pitch variation
        if (audioClips.Length > 0 && audioSource != null)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.pitch = 1f + Random.Range(-audioPitchVariation, audioPitchVariation);
            audioSource.Play();
        }
    }

    private void OnValidate()
    {
        // Ensure minInterval is always less than or equal to maxInterval
        if (minTimeInterval > maxTimeInterval)
        {
            minTimeInterval = maxTimeInterval;
        }

        // Ensure AudioPitchVariation is within a reasonable range
        audioPitchVariation = Mathf.Clamp(audioPitchVariation, 0f, 1f);
    }
}
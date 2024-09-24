using UnityEngine;

public class ToggleSyncAudioSources : MonoBehaviour
{
    // Public fields to assign the audio sources in the inspector
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    // The key to press to start/stop the audio sources
    public KeyCode toggleKey = KeyCode.Alpha6; // Default key is '6'

    private bool isPlaying = false;

    private void Update()
    {
        // Check if the assigned key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            if (isPlaying)
            {
                StopAudioSources();
            }
            else
            {
                StartAudioSources();
            }
        }
    }

    private void StartAudioSources()
    {
        if (audioSource1 != null && audioSource2 != null)
        {
            double startTime = AudioSettings.dspTime + 0.1; // Schedule to start after 0.1 seconds

            // Set both audio sources to the beginning
            audioSource1.Stop();
            audioSource2.Stop();
            audioSource1.time = 0;
            audioSource2.time = 0;

            // Schedule both audio sources to start at the same time
            audioSource1.PlayScheduled(startTime);
            audioSource2.PlayScheduled(startTime);

            isPlaying = true;
        }
        else
        {
            Debug.LogWarning("AudioSource1 or AudioSource2 is not assigned.");
        }
    }

    private void StopAudioSources()
    {
        if (audioSource1 != null && audioSource2 != null)
        {
            // Stop both audio sources
            audioSource1.Stop();
            audioSource2.Stop();

            isPlaying = false;
        }
        else
        {
            Debug.LogWarning("AudioSource1 or AudioSource2 is not assigned.");
        }
    }
}

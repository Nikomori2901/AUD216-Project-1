using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip openUI; 
    public AudioClip closeUI;

    private AudioSource UserInterfaceAudioSource; // Changed to private




    private void Awake()
    {
        // Ensure there's only one instance of SoundManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Get the AudioSource from the child object named "UI AudioSource"
        GameObject uiAudioSourceObject = transform.Find("UI AudioSource").gameObject;
        if (uiAudioSourceObject != null) // Check if the child was found
        {
            UserInterfaceAudioSource = uiAudioSourceObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("UI AudioSource child object not found in SoundManager.");
        }
    }

    // Method to play the pause sound
    public void PlayPauseSound()
    {
        if (UserInterfaceAudioSource != null && openUI != null)
        {
            UserInterfaceAudioSource.PlayOneShot(openUI);
        }
        else
        {
            Debug.LogError("UserInterfaceAudioSource or pauseSound is null.");
        }
    }

    // Method to play the resume sound
    public void PlayResumeSound()
    {
        if (UserInterfaceAudioSource != null && closeUI != null)
        {
            UserInterfaceAudioSource.PlayOneShot(closeUI);
        }
        else
        {
            Debug.LogError("UserInterfaceAudioSource or resumeSound is null.");
        }
    }

    // You can add more methods here to play other sounds as needed
}

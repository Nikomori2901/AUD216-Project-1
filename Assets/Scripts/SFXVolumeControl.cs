using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXVolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the Audio Mixer
    public string sfxVolumeParameter = "SFXVolume"; // Name of the parameter in the Audio Mixer

    public Slider sfxVolumeSlider; // Reference to the Slider UI element

    private void Start()
    {
        // Initialize the Slider's value to the current SFX volume
        if (audioMixer != null)
        {
            float sfxVolume;
            bool result = audioMixer.GetFloat(sfxVolumeParameter, out sfxVolume);
            if (result)
            {
                sfxVolumeSlider.value = Mathf.Pow(10f, sfxVolume / 20f); // Convert from decibels to linear
            }
        }

        // Add a listener to the Slider's value change event
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Method to set the SFX volume based on the Slider's value
    public void SetSFXVolume(float volume)
    {
        if (audioMixer != null)
        {
            // Convert the Slider's value to decibels
            float sfxVolume = Mathf.Log10(volume) * 20f;
            audioMixer.SetFloat(sfxVolumeParameter, sfxVolume);
        }
    }
}

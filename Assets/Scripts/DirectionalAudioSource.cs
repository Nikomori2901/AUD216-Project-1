using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DirectionalAudioSource : MonoBehaviour
{


    [Header("Directivity Controls")]
    [Tooltip("The angle (in degrees) within which the audio has no filtering applied.")]
    [Range(0, 180)]
    public float onAxisAngle = 50f;

    [Tooltip("The angle (in degrees) at which the audio has maximum filtering applied.")]
    [Range(0, 180)]
    public float offAxisAngle = 180f;

    [Header("Off Axis Adjustments")]
    [Tooltip("The frequency of the low pass filter when the listener is at the off-axis angle.")]
    [Range(10, 22000)]
    public float frequency = 1000f;

    [Tooltip("The volume reduction (in decibels) when the listener is at the off-axis angle.")]
    [Range(0, 80)]
    public float volumeReduction = 20f;

    [Header("Debug Information")]
    [Tooltip("The current angle (in degrees) between the audio source's forward direction and the direction to the listener.")]
    //[SerializeField]
    private string currentPlayerAngle = "0.0°";

    [Tooltip("The current frequency of the low pass filter.")]
    // [SerializeField]
    private string currentFilterFrequency = "22000 Hz";

    [Tooltip("The current volume reduction in decibels.")]
    //[SerializeField]
    private string currentVolumeReduction = "0 dB";

    private const float onAxisFrequency = 22000f;
    private AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;
    private float currentAngle;
    private float baseVolume;
    private AudioListener audioListener;

    // Read-Only Properties for Editor Access
    public float CurrentListenerAngle => currentAngle;
    public float CurrentFilterFrequencyValue => lowPassFilter != null ? lowPassFilter.cutoffFrequency : frequency;
    public float CurrentVolumeReductionValue => Mathf.Lerp(0, volumeReduction, Mathf.InverseLerp(onAxisAngle, offAxisAngle, currentAngle));

    // Public Read-Only Property for Player Angle
    public string PlayerAngle => currentPlayerAngle;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
            enabled = false;
            return;
        }

        baseVolume = audioSource.volume;

        lowPassFilter = GetComponent<AudioLowPassFilter>();
        if (lowPassFilter == null)
        {
            lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
        }
    }

    private void Start()
    {
        FindAudioListener();
    }

    private void FindAudioListener()
    {
        audioListener = FindObjectOfType<AudioListener>();
        if (audioListener == null)
        {
            Debug.LogError("AudioListener not found in the scene. Please ensure there is an AudioListener in the scene.");
            enabled = false;
        }
    }

    private void Update()
    {
        if (audioListener != null && lowPassFilter != null)
        {
            UpdateAudioProperties();
        }
        else if (audioListener == null)
        {
            FindAudioListener();
        }
    }

    private void UpdateAudioProperties()
    {
        Vector3 directionToListener = audioListener.transform.position - transform.position;
        currentAngle = Vector3.Angle(transform.forward, directionToListener);
        currentPlayerAngle = $"{currentAngle:F1}°";

        float t = Mathf.InverseLerp(onAxisAngle, offAxisAngle, currentAngle);

        // Update low pass filter
        float cutoffFrequency = Mathf.Lerp(onAxisFrequency, frequency, t);
        lowPassFilter.cutoffFrequency = cutoffFrequency;
        currentFilterFrequency = $"{cutoffFrequency:F0} Hz";

        // Update volume
        float volumeReduction = Mathf.Lerp(0, this.volumeReduction, t);
        audioSource.volume = baseVolume * Mathf.Pow(10, -volumeReduction / 20);
        currentVolumeReduction = $"{volumeReduction:F1} dB";
    }

    private void OnValidate()
    {
        // Ensure offAxisAngle is always greater than or equal to onAxisAngle
        if (offAxisAngle < onAxisAngle)
        {
            offAxisAngle = onAxisAngle;
        }

        // Clamp angles to valid range
        onAxisAngle = Mathf.Clamp(onAxisAngle, 0f, 180f);
        offAxisAngle = Mathf.Clamp(offAxisAngle, 0f, 180f);
    }
}

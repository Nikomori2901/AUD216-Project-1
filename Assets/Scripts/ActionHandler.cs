using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent
using DG.Tweening; // Ensure DOTween is installed and imported

public class ActionHandler : MonoBehaviour
{
    [System.Serializable]
    public class PowerToggleEvent : UnityEvent<bool> { }

    [Header("Events")]
    public PowerToggleEvent onPowerToggled;

    // Existing public fields
    [Header("Audio Settings")]
    [SerializeField]
    private AudioClip toggleSound;

    [Header("Mesh Renderers")]
    [SerializeField]
    private ToggleMeshRenderers meshRendererManager;

    [Header("Switch GameObjects")]
    [SerializeField]
    private GameObject lightSwitchObject;

    [SerializeField]
    private GameObject powerSwitchObject;

    [Header("Control Groups")]
    [SerializeField]
    private GameObject lightGroup;

    [Header("Electrical Sparks")]
    [SerializeField]
    private ElectricalSparksController electricalSparksController;

    [Header("Music Control")]
    [SerializeField]
    private MusicController2 musicController2;

    // Private fields
    private AudioSource audioSource;

    // Flags to track the current state (on or off)
    private bool isAnimating = false;
    private bool isLightSwitchOn = false;
    private bool isPowerSwitchOn = false;

    // Public property to expose the isAnimating flag
    public bool IsAnimating
    {
        get { return isAnimating; }
    }

    void Awake()
    {
        // Existing initialization logic...

        // Initialize Light Group based on Play Mode
        if (lightGroup != null)
        {
            if (Application.isPlaying)
            {
                // Turn off lights at the start of Play Mode
                lightGroup.SetActive(false);
                isLightSwitchOn = false; // Ensure the flag is in sync
                Debug.Log("Light Group initialized to Off for Play Mode.");
            }
            else
            {
                // Ensure lights are on in the Editor
                lightGroup.SetActive(true);
                isLightSwitchOn = true; // Ensure the flag is in sync
                Debug.Log("Light Group initialized to On for Editor.");
            }
        }
        else
        {
            Debug.LogWarning("Light Group is not assigned in ActionHandler.");
        }

        // Initialize Electrical Sparks based on Power State
        if (electricalSparksController != null)
        {
            electricalSparksController.gameObject.SetActive(!isPowerSwitchOn); // Active when power is off
            Debug.Log($"Electrical Sparks initialized to {(electricalSparksController.gameObject.activeSelf ? "Active" : "Inactive")}.");
        }
        else
        {
            Debug.LogWarning("ElectricalSparksController is not assigned in ActionHandler.");
        }
    }

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Subscribe to the PerformAction event
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnPerformAction += PerformAction;
            Debug.Log("ActionHandler subscribed to EventManager events.");
        }
        else
        {
            Debug.LogError("EventManager instance is null. Ensure EventManager is present in the scene.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the PerformAction event to prevent memory leaks
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnPerformAction -= PerformAction;
        }
    }

    // Method to handle actions based on the actionText
    void PerformAction(string actionText, GameObject lightGroupParam, GameObject switchObject)
    {
        switch (actionText.ToLower())
        {
            case "turn on the lights":
                ToggleLights(lightGroupParam, lightSwitchObject);
                break;

            case "play music":
                ToggleMusic();
                break;

            case "turn on the power":
                TogglePower();
                break;

            default:
                Debug.LogWarning($"Action '{actionText}' not recognized.");
                break;
        }
    }

    // Method to toggle lights after light switch rotation
    void ToggleLights(GameObject lightGroupParam, GameObject switchObject)
    {
        if (isAnimating)
        {
            Debug.Log("Toggle action is already in progress. Please wait.");
            return;
        }

        if (switchObject != null)
        {
            Transform switchTransform = switchObject.transform;
            float targetRotation = isLightSwitchOn ? -16f : 1f; // Updated rotation angles

            isAnimating = true; // Indicate that an animation is in progress

            // Use DOTween to rotate smoothly over the set number of seconds with ease-in-out sine
            switchTransform.DOLocalRotate(new Vector3(targetRotation, switchTransform.localEulerAngles.y, switchTransform.localEulerAngles.z), 0.1f)
                          .SetEase(Ease.InOutSine)
                          .OnComplete(() =>
                          {
                              // Toggle the light group's active state
                              if (lightGroupParam != null)
                              {
                                  lightGroupParam.SetActive(!lightGroupParam.activeSelf);
                                  Debug.Log($"Light Group is now {(lightGroupParam.activeSelf ? "On" : "Off")}");
                              }
                              else
                              {
                                  Debug.LogWarning("Light Group is not assigned in ToggleLights.");
                              }

                              // Play toggle sound
                              if (audioSource != null && toggleSound != null)
                              {
                                  audioSource.PlayOneShot(toggleSound);
                                  Debug.Log("Toggle sound played.");
                              }
                              else
                              {
                                  Debug.LogWarning("AudioSource or ToggleSound not assigned in ToggleLights.");
                              }

                              // Update the light switch state
                              isLightSwitchOn = !isLightSwitchOn;
                              Debug.Log($"Light Switch is now {(isLightSwitchOn ? "On" : "Off")}.");

                              isAnimating = false; // Reset the flag to allow new toggle actions
                          });

            Debug.Log("Starting ToggleLights action.");
        }
        else
        {
            Debug.LogWarning("Light Switch Object is not assigned in ToggleLights.");
        }
    }

    // Method to toggle music via MusicController2
    void ToggleMusic()
    {
        if (musicController2 != null)
        {
            musicController2.ToggleMusic();
            Debug.Log("ToggleMusic method called in ActionHandler.");
        }
        else
        {
            Debug.LogWarning("MusicController2 is not assigned in ActionHandler.");
        }
    }

    // Method to toggle power after power switch rotation
    void TogglePower()
    {
        if (isAnimating)
        {
            Debug.Log("Toggle action is already in progress. Please wait.");
            return;
        }

        if (powerSwitchObject != null)
        {
            Transform powerSwitchTransform = powerSwitchObject.transform;
            float targetRotation = isPowerSwitchOn ? 0f : 60f; // Updated rotation angles

            isAnimating = true; // Indicate that an animation is in progress

            // Use DOTween to rotate smoothly over the set number of seconds with ease-in-out sine
            powerSwitchTransform.DOLocalRotate(new Vector3(targetRotation, powerSwitchTransform.localEulerAngles.y, powerSwitchTransform.localEulerAngles.z), 0.5f)
                                 .SetEase(Ease.InOutSine)
                                 .OnComplete(() =>
                                 {
                                     // Toggle the MeshRenderers' emission states
                                     if (meshRendererManager != null)
                                     {
                                         meshRendererManager.ToggleEmissionStates(); // Updated method call
                                         Debug.Log("Mesh Renderers' emission states toggled.");
                                     }
                                     else
                                     {
                                         Debug.LogWarning("MeshRendererManager is not assigned in TogglePower.");
                                     }

                                     // Play toggle sound
                                     if (audioSource != null && toggleSound != null)
                                     {
                                         audioSource.PlayOneShot(toggleSound);
                                         Debug.Log("Toggle sound played.");
                                     }
                                     else
                                     {
                                         Debug.LogWarning("AudioSource or ToggleSound not assigned in TogglePower.");
                                     }

                                     // Update the power switch state
                                     isPowerSwitchOn = !isPowerSwitchOn;
                                     Debug.Log($"Power Switch is now {(isPowerSwitchOn ? "On" : "Off")}.");

                                     // Control Electrical Sparks based on power state
                                     if (electricalSparksController != null)
                                     {
                                         if (isPowerSwitchOn)
                                         {
                                             electricalSparksController.DeactivateSparks(); // Stop sparks when power is on
                                             Debug.Log("Electrical Sparks are deactivating.");
                                         }
                                         else
                                         {
                                             electricalSparksController.ActivateSparks(); // Start sparks when power is off
                                             Debug.Log("Electrical Sparks are activating.");
                                         }
                                     }
                                     else
                                     {
                                         Debug.LogWarning("ElectricalSparksController is not assigned in ActionHandler.");
                                     }

                                     isAnimating = false; // Reset the flag to allow new toggle actions
                                 });

            Debug.Log("Starting TogglePower action.");
        }
        else
        {
            Debug.LogWarning("Power Switch Object is not assigned in TogglePower.");
        }
    }
}

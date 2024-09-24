using UnityEngine;
using TMPro;

public class InteractionSystemTMP : MonoBehaviour
{
    public string actionText = "turn on the power";  // Changed actionText to "turn on the power"
    public GameObject objectToInteractWith;           // Optional, depending on the action
    public GameObject switchObject;                  // Optional, depending on the action
    public AudioClip toggleSound;                    // Optional, handled in ActionHandler

    public ActionHandler actionHandler;              // Assign in Inspector

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (actionHandler != null && !actionHandler.IsAnimating)
            {
                // Trigger the PerformAction event via EventManager
                EventManager.Instance.PerformAction(actionText, objectToInteractWith, switchObject);
            }
            else
            {
                Debug.Log("Action is currently animating. Please wait.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            string promptMessage = $"Press E to {actionText}";
            // Trigger the ShowInteractionPrompt event
            EventManager.Instance.ShowInteractionPrompt(promptMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Trigger the HideInteractionPrompt event
            EventManager.Instance.HideInteractionPrompt();
        }
    }
}

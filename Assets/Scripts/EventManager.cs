using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    // Singleton instance
    public static EventManager Instance;

    // Existing events
    public event Action<string, GameObject, GameObject> OnPerformAction;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Existing PerformAction method
    public void PerformAction(string actionText, GameObject lightGroup, GameObject switchObject)
    {
        OnPerformAction?.Invoke(actionText, lightGroup, switchObject);
        Debug.Log($"Action '{actionText}' performed.");
    }

    // Add ShowInteractionPrompt method
    public void ShowInteractionPrompt(string promptText)
    {
        UIManager.Instance.DisplayPrompt(promptText);
        Debug.Log($"Interaction Prompt Shown: {promptText}");
    }

    public void HideInteractionPrompt()
    {
        UIManager.Instance.HidePrompt();
        Debug.Log("Interaction Prompt Hidden.");
    }
}

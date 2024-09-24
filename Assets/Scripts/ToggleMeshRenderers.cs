using UnityEngine;
using System.Collections.Generic;

public class ToggleMeshRenderers : MonoBehaviour
{
    // Array to hold MeshRenderers
    public MeshRenderer[] meshRenderers;

    // A flag to track the current emission state (active or not)
    private bool isEmissionActive = true;

    void Awake()
    {
        if (Application.isPlaying)
        {
            // Initialize emission to off when the game starts
            isEmissionActive = false;
            SetEmissionStates(isEmissionActive);
            Debug.Log("Emission states initialized to Off for Play Mode.");
        }
        else
        {
            // Ensure emission is on in the Editor for development convenience
            isEmissionActive = true;
            SetEmissionStates(isEmissionActive);
            Debug.Log("Emission states initialized to On for Editor.");
        }
    }

    // Public method to toggle Emission states
    public void ToggleEmissionStates()
    {
        // Toggle the emission state
        isEmissionActive = !isEmissionActive;

        // Toggle emission for each MeshRenderer
        SetEmissionStates(isEmissionActive);

        Debug.Log($"Emission states toggled to {(isEmissionActive ? "On" : "Off")}.");
    }

    // Public method to initialize emission states (called from ActionHandler.cs Awake)
    public void InitializeEmissionStates()
    {
        if (!Application.isPlaying)
        {
            // Ensure that emission is on in the Editor
            isEmissionActive = true;
            SetEmissionStates(isEmissionActive);
            Debug.Log("Emission states explicitly initialized to On for Editor.");
        }
        // No action needed for Play Mode as it's handled in Awake()
    }

    // Helper method to set emission states based on the flag
    private void SetEmissionStates(bool state)
    {
        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer != null)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (state)
                    {
                        mat.EnableKeyword("_EMISSION"); // Enable emission keyword
                        mat.SetColor("_EmissionColor", Color.white); // Set emission color (adjust as needed)
                    }
                    else
                    {
                        mat.DisableKeyword("_EMISSION"); // Disable emission keyword
                        mat.SetColor("_EmissionColor", Color.black); // Set emission color to black to turn it off
                    }
                }
            }
            else
            {
                Debug.LogWarning("MeshRenderer is not assigned in ToggleMeshRenderers.");
            }
        }
    }
}

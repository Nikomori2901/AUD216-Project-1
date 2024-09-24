using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public string areaName;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("TriggerManager: Player entered trigger for area: " + areaName);
            if (audioManager != null)
            {
                audioManager.ChangeArea(areaName);
            }
            else
            {
                //Debug.LogError("TriggerManager: AudioManager not found in the scene.");
            }
        }
    }
}

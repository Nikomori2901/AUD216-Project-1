using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerTest: Entered trigger with: " + other.name);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("TriggerTest: Staying in trigger with: " + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerTest: Exited trigger with: " + other.name);
    }
}

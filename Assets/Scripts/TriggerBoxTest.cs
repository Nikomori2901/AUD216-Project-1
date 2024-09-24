using UnityEngine;

public class TriggerBoxTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerBoxTest: Entered trigger with: " + other.name);
    }
}

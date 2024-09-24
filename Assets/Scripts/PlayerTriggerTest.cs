using UnityEngine;

public class PlayerTriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerTriggerTest: Entered trigger with: " + other.name);
    }
}

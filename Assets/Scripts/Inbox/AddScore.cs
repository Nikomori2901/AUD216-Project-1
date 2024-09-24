using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ScoringSystem.theScore += 1;
    }
}

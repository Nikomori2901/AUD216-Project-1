using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour

{
    public static int theScore;
    public GameObject scoreText;
    public GameObject activateChestText;
    public GameObject activateChest;

    void Update()
    {
        scoreText.GetComponent<Text>().text = "Pickups: " + theScore + "/5";

        if (theScore == 4)
        {
            activateChestText.SetActive(true);
            activateChest.SetActive(true);
        }
    }
}
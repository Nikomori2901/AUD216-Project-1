using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour

{
    public static int theScore;
    public GameObject scoreText;
    //public GameObject activateChestText;
    //public GameObject activateChest;
    public CaveEntrance caveEntrance;
    //public GameObject caveOpenText;

    void Update()
    {
        scoreText.GetComponent<Text>().text = "Pickups: " + theScore + "/4";

        if (theScore == 4)
        {
            //activateChestText.SetActive(true);
            //activateChest.SetActive(true);

            //caveOpenText.SetActive(true);
            UnlockCave();
        }
    }

    private void UnlockCave()
    {
        theScore = 0;
        caveEntrance.StartOpening();
    }
}
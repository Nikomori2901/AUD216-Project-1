using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

      public void MainMenu()
    {
        SceneManager.LoadScene("Level for Sound Design");
    }



    void Update()
    {
        bool restart = Input.GetKeyDown(KeyCode.L);
        if (restart)
        {
            SceneManager.LoadScene("Level for Sound Design");
        }
    }
}

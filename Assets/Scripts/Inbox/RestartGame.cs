using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScoringSystem.theScore = 0;
            SceneManager.LoadScene("Level for Sound Design");
        }        
    }
}

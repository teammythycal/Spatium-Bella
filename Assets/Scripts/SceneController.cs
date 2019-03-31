using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int sceneNumber = 0;

    public void LoadNextLevel()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        
        if(sceneNumber != 3)
        {
            sceneNumber++;
            SceneManager.LoadScene("level" + sceneNumber);
        }
        else
        {
            Debug.Log("Invalid scene index. Returning to title screen.");
            LoadTitle();
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("titleScreen");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausePanel; 
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void TogglePause() 
    {
        if (isPaused)
        {
            Time.timeScale = 1f; 
            isPaused = false;
            pausePanel.SetActive(false); 
        }
        else
        {
            Time.timeScale = 0f; 
            isPaused = true;
            pausePanel.SetActive(true); 
        }
    }
    
}

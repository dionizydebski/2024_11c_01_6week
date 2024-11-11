using System.Collections;
using System.Collections.Generic;
using LevelMenager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    
    public void SaveGame()
    {
        SimpleSaveSystem.SaveXML();
    }

    public void LoadGame()
    {
        SimpleSaveSystem.LoadXML();
    }
    
    public void Settings()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

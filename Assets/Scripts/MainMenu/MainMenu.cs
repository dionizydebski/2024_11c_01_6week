using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenLevelMenu()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenPlayButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

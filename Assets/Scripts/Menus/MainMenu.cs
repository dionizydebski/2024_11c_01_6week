
using System.Collections;
using LevelMenager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject settingsMenuUI;
        public GameObject mainMenuUI;

        /*private void Awake()
        {
            _continue.SetActive(PlayerPrefs.HasKey("LastLevel"));
        }*/

        public void PlayGame()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("LastLevel", "Level 0");
            SceneManager.LoadSceneAsync(3);
        }

        public void ContinueClicked()
        {
            string lastLevel = PlayerPrefs.GetString("LastLevel");
            SceneManager.LoadScene(lastLevel);
            
            SimpleSaveSystem.LoadXML();
        }

        public void OpenLevelMenu()
        {
            SceneManager.LoadSceneAsync(2);
        }
        
        public void Settings()
        {
            settingsMenuUI.SetActive(true);
            mainMenuUI.SetActive(false);
        }
        
        public void BackToMainMenu()
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }

        public void QuitGame()
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}

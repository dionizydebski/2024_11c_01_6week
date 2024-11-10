using System;
using LevelMenager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string _levelToLoad;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPrefs.SetString("LastLevel", _levelToLoad);
        SceneManager.LoadSceneAsync(_levelToLoad);
        SimpleSaveSystem.SaveXML();
    }
}

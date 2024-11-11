using System;
using LevelMenager;
using UnityEngine;
using UnityEngine.Serialization;

public class CloseWindow : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    private bool _closed = false;

    private void Awake()
    {
        EventSystem.OnLoadGame += LoadGame;
        EventSystem.OnSaveGame += SaveGame;
    }

    private void Update()
    {
        //Debug.Log(_closed);
        if (!_closed)
        {
            if (time > 0) time -= Time.deltaTime;
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                _closed = true;
            }
        }
    }

    public void Close()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventSystem.OnLoadGame -= LoadGame;
        EventSystem.OnSaveGame -= SaveGame;
    }

    private void LoadGame(SaveData data)
    {
        _closed = true;
    }

    private void SaveGame(SaveData data)
    {
        _closed = true;
    }
}

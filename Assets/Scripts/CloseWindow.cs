using System;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    private float _time = 1f;
    private bool _closed = false;
    private void Update()
    {
        if (!_closed)
        {
            if (_time > 0) _time -= Time.deltaTime;
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                _closed = true;
            }
        }
    }

    public void Close()
    {
        Debug.Log(gameObject.transform.GetChild(0).gameObject.name);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}

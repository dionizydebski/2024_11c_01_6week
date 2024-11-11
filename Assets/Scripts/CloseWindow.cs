using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CloseWindow : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    private bool _closed = false;
    private void Update()
    {
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
}

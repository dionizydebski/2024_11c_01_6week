using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private PlayerHealth _playerHealth;
    
    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        SetHealth(_playerHealth.getCurrentHealth());
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

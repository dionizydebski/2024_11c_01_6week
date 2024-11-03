using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private PlayerHealth _playerHealth;

    /*private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        slider.maxValue = _playerHealth.GetMaxHealth();
        slider.value = _playerHealth.GetCurrentHealth();
        
        
    }*/

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        Debug.LogError("Updated health bar");
    }

    public void SetHealthValue(float health)
    {
        slider.value = health;
    }
}

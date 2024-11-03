using Player;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Text healthText; 
        [SerializeField] private float lowHealthThreshold = 3;
        private PlayerHealth _playerHealth;
        private PlayerInventory playerInventory;

        private void Start()
        {
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        
            if (_playerHealth == null)
            {
                Debug.LogError("Health component not found in the Player object.");
            }

            UpdateHealthText();
        }

        private void Update()
        {
            UpdateHealthText();
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInventory.UseHealthPotion(); 
            }
        }

        private void UpdateHealthText()
        {
            if (_playerHealth != null)
            {
                if (_playerHealth.GetCurrentHealth() < lowHealthThreshold && playerInventory.GetHealthPotions() > 0)
                {
                    healthText.text = "E"; 
                    healthText.enabled = true;
                }
                else
                {
                    healthText.enabled = false;
                }
            }
        }
    }
}
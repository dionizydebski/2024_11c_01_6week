using Player;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Text healthText; 
        [SerializeField] private float lowHealthThreshold = 3;
        private PlayerHealth playerHealth;
        private PlayerInventory playerInventory;

        private void Start()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        
            if (playerHealth == null)
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
            if (playerHealth != null)
            {
                if (playerHealth.getCurrentHealth() < lowHealthThreshold && playerInventory.GetHealthPotions() > 0)
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
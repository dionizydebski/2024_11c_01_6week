using Player;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Text healthText;  // Przypisz ten Text w Inspectorze
        [SerializeField] private float lowHealthThreshold = 30f; // Ustal próg zdrowia
        private Health playerHealth;
        private PlayerInventory playerInventory;

        private void Start()
        {
            // Znajdź komponent Health i PlayerInventory w obiekcie Player
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
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

            // Sprawdź, czy klawisz "E" został naciśnięty
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInventory.UseHealthPotion(); // Użyj mikstury zdrowia
            }
        }

        private void UpdateHealthText()
        {
            if (playerHealth != null)
            {
                // Sprawdź, czy zdrowie jest poniżej progu
                if (playerHealth.getCurrentHealth() < lowHealthThreshold)
                {
                    healthText.text = "E"; // Wyświetl "E"
                    healthText.enabled = true; // Włącz widoczność tekstu
                }
                else
                {
                    healthText.enabled = false; // Ukryj tekst, gdy zdrowie jest powyżej progu
                }
            }
        }
    }
}
using HUD;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int coins = 0;
        [SerializeField] private int diamonds = 0;
        [SerializeField] private int healthPotions = 0;

        private PlayerHUD playerHUD;
        private Health playerHealth;
        
        private void Start()
        {
            playerHUD = FindObjectOfType<PlayerHUD>();
            playerHealth = FindObjectOfType<Health>();
        }
        public void AddCoin()
        {
            coins++;
            Debug.Log("Coins collected: " + coins);
            playerHUD.UpdateCoinsHUD(coins);
        }

        public void AddDiamond()
        {
            diamonds++;
            playerHUD.UpdateDiamondsHUD(diamonds);
            Debug.Log("Diamonds collected: " + diamonds);
        }
        
        public void AddHealthPotion()
        {
            healthPotions++;
            playerHUD.UpdateHealthPotionsHUD(healthPotions);
            Debug.Log("Hpotions collected: " + healthPotions);
        }
        
        public bool UseHealthPotion()
        {
            if (healthPotions > 0 && playerHealth != null)
            {
                healthPotions--;
                playerHealth.AddHealth(1);
                playerHUD.UpdateHealthPotionsHUD(healthPotions);
                Debug.Log("Used a health potion. Remaining: " + healthPotions);
                return true;
            }
            Debug.Log("No health potions available!");
            return false;
        }

        public int GetCoins()
        {
            return coins;
        }

        public int GetDiamonds()
        {
            return diamonds;
        }
        
        public int GetHealthPotions()
        {
            return healthPotions;
        }
    }
}
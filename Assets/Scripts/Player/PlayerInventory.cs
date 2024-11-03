using HUD;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int coins = 0;
        [SerializeField] private int diamonds = 0;
        [SerializeField] private int healthPotions = 0;
        [SerializeField] private int skulls = 0;
        [SerializeField] private int keys = 0;
        
        private PlayerHUD _playerHUD;
        private PlayerHealth _playerHealth;
        
        private void Start()
        {
            _playerHUD = FindObjectOfType<PlayerHUD>();
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }
        public void AddCoin()
        {
            coins++;
            Debug.Log("Coins collected: " + coins);
            _playerHUD.UpdateCoinsHUD(coins);
        }

        public void AddDiamond()
        {
            diamonds++;
            _playerHUD.UpdateDiamondsHUD(diamonds);
            Debug.Log("Diamonds collected: " + diamonds);
        }
        
        public void AddHealthPotion()
        {
            healthPotions++;
            _playerHUD.UpdateHealthPotionsHUD(healthPotions);
            Debug.Log("Hpotions collected: " + healthPotions);
        }
        
        public void AddSkull()
        {
            skulls++;
            _playerHUD.UpdateSkullsHUD(skulls);
            Debug.Log("skulls collected: " + skulls);
        }
        
        public void AddKey()
        {
            keys++;
            _playerHUD.UpdateKeysHUD(keys);
            Debug.Log("keys collected: " + keys);
        }
        
        public bool UseHealthPotion()
        {
            if (healthPotions > 0 && _playerHealth != null)
            {
                healthPotions--;
                _playerHealth.AddHealth(1); 
                _playerHUD.UpdateHealthPotionsHUD(healthPotions);
                Debug.Log("Used a health potion. Remaining: " + healthPotions);
                return true;
            }
            Debug.Log("No health potions available");
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
        
        public int GetSkulls()
        {
            return skulls;
        }
        
        public int GetKeys()
        {
            return keys;
        }
    }
}
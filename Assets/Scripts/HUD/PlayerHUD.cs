using Player;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Text coinsText;
        [SerializeField] private Text diamondsText;
        [SerializeField] private Text healthPotionsText;

        private PlayerInventory playerInventory;

        private void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            UpdateCoinsHUD(playerInventory.GetCoins());
            UpdateDiamondsHUD(playerInventory.GetDiamonds());
            UpdateHealthPotionsHUD(playerInventory.GetHealthPotions());
        }

        public void UpdateCoinsHUD(int coinCount)
        {
            coinsText.text = coinCount.ToString();
        }

        public void UpdateDiamondsHUD(int diamondCount)
        {
            diamondsText.text = diamondCount.ToString();
        }

        public void UpdateHealthPotionsHUD(int healthPotionCount)
        {
            healthPotionsText.text = healthPotionCount.ToString();
        }
    }
}
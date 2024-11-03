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
        [SerializeField] private Text skullText;
        [SerializeField] private Text keyText;

        private PlayerInventory _playerInventory;

        private void Start()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            UpdateCoinsHUD(_playerInventory.GetCoins());
            UpdateDiamondsHUD(_playerInventory.GetDiamonds());
            UpdateHealthPotionsHUD(_playerInventory.GetHealthPotions());
            UpdateSkullsHUD(_playerInventory.GetSkulls());
            UpdateKeysHUD(_playerInventory.GetKeys());
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
        
        public void UpdateSkullsHUD(int skullsCount)
        {
            skullText.text = skullsCount.ToString();
        }
        
        public void UpdateKeysHUD(int keysCount)
        {
            keyText.text = keysCount.ToString();
        }
    }
}
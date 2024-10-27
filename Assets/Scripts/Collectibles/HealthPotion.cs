using Player;
using UnityEngine;
using static Health;

namespace Collectibles
{
    public class HealthPotion : Collectible
    {
        [SerializeField] private HealthPotionConfig _healthPotionConfig;

        protected override void Collect()
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddHealthPotion();
            }

            /*Health playerHealth = FindObjectOfType<Health>();
            if (playerHealth != null)
            {
               // Health.AddHealth(_healthPotionConfig.HealAmount);
            }*/
            
            Destroy(gameObject);
        }
    }
}
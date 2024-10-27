using UnityEngine;
using static Health;

namespace Collectibles
{
    public class HealthPotion : Collectible
    {
        [SerializeField] private HealthPotionConfig _healthPotionConfig;

        protected override void Collect()
        {
            Health playerHealth = FindObjectOfType<Health>();
            if (playerHealth != null)
            {
               // Health.AddHealth(_healthPotionConfig.HealAmount);
            }
            
            Destroy(gameObject);
        }
    }
}
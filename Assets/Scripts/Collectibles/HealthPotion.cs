using Player;
using UnityEngine;
using static Health;

namespace Collectibles
{
    public class HealthPotion : Collectible
    {
        [SerializeField] private HealthPotionConfig _healthPotionConfig;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        protected override void Collect()
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddHealthPotion();
                _animator.SetBool("isPickedUp", true);
                Invoke(nameof(DestroyPotion), 0.5f);
            }
            else
            {
                Debug.LogError("PlayerInventory not found! Health Potion collection failed.");
            }
        }
        private void DestroyPotion()
        {
            Destroy(gameObject);
        } 
    }
}
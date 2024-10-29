using UnityEngine;
using Player;

namespace Collectibles
{
    public class Coin : Collectible
    {
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
                playerInventory.AddCoin();
                _animator.SetBool("isPickedUp", true);
                Invoke(nameof(DestroyCoin), 0.5f);
            }
            else
            {
                Debug.LogError("PlayerInventory not found! Coin collection failed.");
            }
        }
        
        private void DestroyCoin()
        {
            Destroy(gameObject);
        } 
    }
}
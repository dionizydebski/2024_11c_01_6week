using Player;
using UnityEngine;

namespace Collectibles 
{
    public class Key : Collectible
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
                playerInventory.AddKey();
                _animator.SetBool("isPickedUp", true);
                Invoke(nameof(DestroyKey), 0.5f);
            }
            else
            {
                Debug.LogError("PlayerInventory not found! Coin collection failed.");
            }
        }
        
        private void DestroyKey()
        {
            Destroy(gameObject);
        } 
    }
}
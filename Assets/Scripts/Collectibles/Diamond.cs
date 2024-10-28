using UnityEngine;
using Player;

namespace Collectibles
{
    public class Diamond : Collectible
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
                playerInventory.AddDiamond();
                _animator.SetBool("isPickedUp", true);
                Invoke(nameof(DestroyDiamond), 0.5f);
            }
            else
            {
                Debug.LogError("PlayerInventory not found! Coin collection failed.");
            }
        }
        
        private void DestroyDiamond()
        {
            Destroy(gameObject);
        } 
    }
}
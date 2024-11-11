using Player;
using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(Collider2D))]
    public class Collectible : MonoBehaviour
    {
        public enum CollectibleType { Coin, Diamond, HealthPotion, Key, Skull }
        public CollectibleType collectibleType;
        
        private AudioManager _audioManager;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _audioManager.PlaySFX(_audioManager.pickUp);
                var playerInventory = other.GetComponent<PlayerInventory>();

                if (playerInventory != null)
                {
                    CollectItem(playerInventory);
                    _animator.SetBool("isPickedUp", true);
                    Invoke(nameof(DestroyCollectible), 0.5f);
                }
                else
                {
                    Debug.LogError("PlayerInventory not found! Collection failed.");
                }
            }
        }

        private void CollectItem(PlayerInventory playerInventory)
        {
            switch (collectibleType)
            {
                case CollectibleType.Coin:
                    playerInventory.AddCoin();
                    break;
                case CollectibleType.Diamond:
                    playerInventory.AddDiamond();
                    break;
                case CollectibleType.HealthPotion:
                    playerInventory.AddHealthPotion();
                    break;
                case CollectibleType.Key:
                    playerInventory.AddKey();
                    break;
                case CollectibleType.Skull:
                    playerInventory.AddSkull();
                    break;
                default:
                    Debug.LogWarning("Unknown collectible type!");
                    break;
            }
        }

        private void DestroyCollectible()
        {
            Destroy(gameObject);
        }
    }
}

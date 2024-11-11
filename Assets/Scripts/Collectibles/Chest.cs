using UnityEngine;
using Collectibles;
using Player;

namespace Collectibles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Chest : MonoBehaviour
    {
        public GameObject[] itemPrefabs;
        public int itemsToSpawn = 3;
        public AudioClip openSound;
        private bool _isOpened = false;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isOpened && other.CompareTag("Player"))
            {
                var playerInventory = other.GetComponent<PlayerInventory>();

                if (playerInventory != null && playerInventory.GetKeys() > 0)
                {
                    OpenChest(playerInventory);
                }
                else
                {
                    Debug.Log("Brak klucza! Nie można otworzyć skrzyni.");
                }
            }
        }

        private void OpenChest(PlayerInventory playerInventory)
        {
            _isOpened = true;
            playerInventory.UseKey();
            AudioSource.PlayClipAtPoint(openSound, transform.position);
            _animator.SetTrigger("Open");
            
            Invoke(nameof(SpawnItems), 0.5f);
            Destroy(gameObject, 1f);
        }

        private void SpawnItems()
        {
            for (int i = 0; i < itemsToSpawn; i++)
            {
                if (itemPrefabs.Length > 0)
                {
                    int randomIndex = Random.Range(0, itemPrefabs.Length);
                    GameObject item = Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
                    
                    Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 force = new Vector2(Random.Range(-1f, 1f), 1f) * 5f;
                        rb.AddForce(force, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}

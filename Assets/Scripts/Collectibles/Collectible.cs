using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Collectible : MonoBehaviour
    {
        public AudioClip pickUpSound;
        
        protected abstract void Collect();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                Collect();
            }
        }
    }
}
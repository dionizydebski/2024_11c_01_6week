using UnityEngine;

namespace enemy
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private Health playerHealth;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
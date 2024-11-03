using UnityEngine;
using UnityEngine.Serialization;

namespace enemy
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private int damage;
        [FormerlySerializedAs("playerHealth")] [SerializeField] private Health.HealthScript playerHealthScript;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                playerHealthScript.TakeDamage(damage);
            }
        }
    }
}
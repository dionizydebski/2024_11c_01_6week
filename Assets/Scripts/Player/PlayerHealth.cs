using System.Collections;
using Health;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : HealthScript
    {
        [SerializeField] private GameObject sword;
        [SerializeField] private Behaviour[] components;

        protected override IEnumerator WaitAndDie()
        {
            ItemDrop();
            // Czekaj 5 sekund
            yield return new WaitForSeconds(5);

        }

        public void Respawn()
        {
            dead = false;
            AddHealth(maxHealth);
            Anim.ResetTrigger("die");
            Anim.Play("Idle");
            StartCoroutine(Invunerability());

            foreach (var component in components)
            {
                component.enabled = true;
            }
        }

        private void ItemDrop()
        {
            Instantiate(sword, transform.position + Vector3.right, Quaternion.identity);
        }
    }
}


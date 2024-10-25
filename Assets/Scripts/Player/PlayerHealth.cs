using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health.Health
    {
        [SerializeField] private GameObject sword;
        [SerializeField] private Behaviour[] components;
        [SerializeField] private float knockbackForce;
        protected override IEnumerator WaitAndDie()
        {
            ItemDrop();
            // Czekaj 5 sekund
            yield return new WaitForSeconds(5f);
        }

        public void Respawn()
        {
            Dead = false;
            AddHealth(maxHealth);
            Anim.ResetTrigger("die");
            Anim.Play("Idle");
            StartCoroutine(Invunerability());

            foreach (var component in components)
            {
                component.enabled = true;
            }

        }
        public override void TakeDamage(float _damage)
        {
            if (Invulnerable) return;
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

            if (currentHealth > 0)
            {
                Anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
            }
            else
            {
                if (!Dead)
                {
                    Anim.SetTrigger("die");

                    foreach (var component in components)
                    {
                        component.enabled = false;
                    }

                    Dead = true;
                    StartCoroutine(WaitAndDie());
                }
            }
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void ItemDrop()
        {
            Instantiate(sword, transform.position + Vector3.right, Quaternion.identity);
        }
    }
}


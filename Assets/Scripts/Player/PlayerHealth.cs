using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health.Health
    {
        [SerializeField] private GameObject sword;
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

            //Deactivate all attached component classes
            if (GetComponentInParent<EnemyPatrol>() != null)
            {
                GetComponentInParent<EnemyPatrol>().enabled = true;
            }

            if (GetComponent<MeleeEnemy>() != null)
            {
                GetComponent<MeleeEnemy>().enabled = true;
            }

            if (GetComponentInParent<PlayerAttack>() != null)
            {
                GetComponentInParent<PlayerAttack>().enabled = true;
            }

            if (GetComponent<BasicPlayerMovement>() != null)
            {
                GetComponent<BasicPlayerMovement>().enabled = true;
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


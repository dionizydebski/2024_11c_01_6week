using UnityEngine;
using System.Collections;
using Player;

namespace Health
{
    public class Health : MonoBehaviour
    {
        [Header ("Health")]
        [SerializeField] protected float maxHealth;

        [SerializeField] protected float currentHealth;

        [Header("Other")]
        protected Animator Anim;
        protected bool Dead;
        protected bool Invulnerable;

        [Header("iFrames")]
        [SerializeField] private float iFramesDuration;
        [SerializeField] private int numberOfFlashes;
        private SpriteRenderer _spriteRend;

        protected void Awake()
        {
            currentHealth = maxHealth;
            Anim = GetComponent<Animator>();
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        public virtual void TakeDamage(float _damage)
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

                    //Deactivate all attached component classes
                    if (GetComponentInParent<EnemyPatrol>() != null)
                    {
                        GetComponentInParent<EnemyPatrol>().enabled = false;
                    }

                    if (GetComponent<MeleeEnemy>() != null)
                    {
                        GetComponent<MeleeEnemy>().enabled = false;
                    }

                    if (GetComponentInParent<PlayerAttack>() != null)
                    {
                        GetComponentInParent<PlayerAttack>().enabled = false;
                    }

                    if (GetComponent<BasicPlayerMovement>() != null)
                    {
                        GetComponent<BasicPlayerMovement>().enabled = false;
                    }

                    Dead = true;
                    StartCoroutine(WaitAndDie());

                }
            }
        }

        protected virtual IEnumerator WaitAndDie()
        {
            // Czekaj 5 sekund
            yield return new WaitForSeconds(2f);

            // Po 5 sekundach wykonaj akcję
            Destroy(gameObject);
        }

        public void AddHealth(float _value)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
        }
        protected IEnumerator Invunerability()
        {
            Invulnerable = true;
            Physics2D.IgnoreLayerCollision(10, 11, true);
            for (int i = 0; i < numberOfFlashes; i++)
            {
                _spriteRend.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                _spriteRend.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }
            Physics2D.IgnoreLayerCollision(10, 11, false);
            Invulnerable = false;
        }
    }
}

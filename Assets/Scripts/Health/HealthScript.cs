using UnityEngine;
using System.Collections;
using Player;

namespace Health
{
    public class HealthScript : MonoBehaviour
    {
        private static readonly int Hurt = Animator.StringToHash("hurt");
        private static readonly int Die = Animator.StringToHash("die");

        [Header ("Health")]
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float currentHealth;

        [Header("Other")]
        [SerializeField] private float knockBackForce;
        protected Animator Anim;
        private Rigidbody2D _rigidbody;
        protected bool Dead;
        protected bool Invulnerable;
        private bool _hit;
        private Vector2 _hitDirection;

        [Header("iFrames")]
        [SerializeField] private float iFramesDuration;
        [SerializeField] private int numberOfFlashes;
        private SpriteRenderer _spriteRend;

        public HealthBar healthBar;

        protected void Awake()
        {
            currentHealth = maxHealth;
            Anim = GetComponent<Animator>();
            _spriteRend = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            
            if(gameObject.tag == "Player")
                healthBar.SetMaxHealth(maxHealth);
        }

        private void FixedUpdate()
        {
            if (_hit)
            {
                Vector2 knockBackDirection = new Vector2(Mathf.Sign(_hitDirection.x - transform.position.x) * -1, 1);
                _hit = false;
                KnockBack(knockBackDirection);
            }
        }

        public virtual void TakeDamage(float damage)
        {
            if (Invulnerable) return;
            _hit = true;
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            healthBar.SetHealthValue(currentHealth);
            
            if (currentHealth > 0)
            {
                Anim.SetTrigger(Hurt);
                StartCoroutine(Invunerability());
            }
            else
            {
                if (!Dead)
                {
                    Anim.SetTrigger(Die);

                    //Deactivate all attached component classes
                    if (GetComponentInParent<EnemyPatrol>() != null)
                    {
                        GetComponentInParent<EnemyPatrol>().enabled = false;
                    }

                    if (GetComponent<MeleeEnemy1>() != null)
                    {
                        GetComponent<MeleeEnemy1>().enabled = false;
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

        public void TakeDamage(float damage, Vector2 hitDirection)
        {
            _hitDirection = hitDirection;
            TakeDamage(damage);
        }

        protected virtual IEnumerator WaitAndDie()
        {
            // Czekaj 5 sekund
            yield return new WaitForSeconds(2f);

            // Po 5 sekundach wykonaj akcję
            Destroy(gameObject);
        }

        public void AddHealth(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
            healthBar.SetHealthValue(currentHealth);
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

        private void KnockBack(Vector2 knockBack)
        {
            if (gameObject.GetComponent<MeleeEnemy1>() != null) gameObject.GetComponent<MeleeEnemy1>().knockedBack = true;

            _rigidbody.MovePosition(_rigidbody.position + (knockBack * (knockBackForce * Time.fixedDeltaTime)));
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
    }
}


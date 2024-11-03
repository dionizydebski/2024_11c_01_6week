using System.Collections;
using Health;
using UnityEngine;

namespace Platforms
{
    public class PlatformHealth : HealthScript
    {
        private static readonly int Destroy1 = Animator.StringToHash("Destroy");
        [SerializeField] private float destroyTime = 2f;

        public override void TakeDamage(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Anim.SetTrigger(Destroy1);
                StartCoroutine(WaitAndDie());
            }
        }

        protected override IEnumerator WaitAndDie()
        {
            yield return new WaitForSeconds(destroyTime);
            Destroy(gameObject);
        }
    }
}

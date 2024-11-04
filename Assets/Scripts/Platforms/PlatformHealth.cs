using System.Collections;
using Health;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Platforms
{
    public class PlatformHealth : HealthScript
    {
        private static readonly int Destroy1 = Animator.StringToHash("Destroy");
        [SerializeField] private float destroyTime = 2f;
        [SerializeField] private AttackTypes attackType;

        public void TakeDamage(float damage, AttackTypes usedAttackType)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            if (currentHealth <= 0 && attackType == usedAttackType)
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

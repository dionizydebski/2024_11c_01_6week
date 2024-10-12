using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField]private LayerMask enemyLayer;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }

        }

        private void Attack()
        {
            Debug.Log("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                //TODO: Add deal damage component
                Debug.Log("We hit" + enemy);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}


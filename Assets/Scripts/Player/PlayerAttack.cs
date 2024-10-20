using System;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;

        private bool _jumpSlamed;

        [Header("Melee Attack")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private int damage;

        [Header("JumpSlam")]
        [SerializeField] private float jumpSlamForce = 10;
        [SerializeField] private Transform jumpSlamPoint;
        [SerializeField] private float jumpSlamBoxWidth = 0.5f;
        [SerializeField] private float jumpSlamBoxHeight = 0.5f;
        [SerializeField] private float jumpSlamBoxPositionOffset = 0.5f;

        public Health health;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }

            if(InAir())
                if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.S) ||
                    Input.GetButton("Fire1") && Input.GetKeyDown(KeyCode.S))
                {
                    _jumpSlamed = true;
                    _rigidbody.AddForce(Vector2.down * jumpSlamForce, ForceMode2D.Impulse);
                }

            if ((CollideWithEnemy() || !InAir()) && _jumpSlamed) //TODO: Fix hit regitration after jumping onto enemy
            {
                JumpSlam();
                _jumpSlamed = false;
            }

        }

        private void Attack()
        {
            //Player Attack animation goes here
            _animator.SetTrigger("meleeAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                //TODO: Add deal damage component
                health.TakeDamage(damage);
                Debug.Log("We hit" + enemy);
            }
        }

        private void JumpSlam()
        {
            Debug.Log("Ground attack");
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(jumpSlamPoint.position + new Vector3(0,jumpSlamBoxPositionOffset, 0), new Vector3(jumpSlamBoxWidth, jumpSlamBoxHeight, 0), 0f,enemyLayer);

            Debug.Log(hitEnemies.Length);
            foreach (var enemy in hitEnemies)
            {
                //TODO: Add deal damage component
                Debug.Log("We hit" + enemy + "with slam");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null && jumpSlamPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            Gizmos.DrawWireCube(jumpSlamPoint.position + new Vector3(0,jumpSlamBoxPositionOffset, 0) , new Vector3(jumpSlamBoxWidth, jumpSlamBoxHeight, 0));
        }

        private bool InAir()
        {
            RaycastHit2D reycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,0, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
            return reycastHit.collider == null;
        }

        private bool CollideWithEnemy()
        {
            RaycastHit2D reycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,0, Vector2.down, 0.1f, LayerMask.GetMask("Enemy"));
            return reycastHit.collider != null;
        }
    }
}


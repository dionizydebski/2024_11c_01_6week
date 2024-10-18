using System;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Serialization;


//TODO: Delete Debug.Log-s before final build
//TODO: Find better way to holding projectiles so that it can be attached to player prefab
namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private BasicPlayerMovement _basicPlayerMovement;

        private bool _jumpSlamed;

        [Header("Melee Attack")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private float meleeAttackCooldown;
        private float _meleeAttackCooldownTimer = Mathf.Infinity;
        [SerializeField]private LayerMask enemyLayer;
        
        [Header("Ranged Attack")]
        [SerializeField] private float rangedAttackCooldown;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject[] projectilePrefabs;
        private float _rangedAttackCooldownTimer = Mathf.Infinity;
        
        [Header("JumpSlam")]
        [SerializeField] private float jumpSlamForce = 10;
        [SerializeField] private Transform jumpSlamPoint;
        [SerializeField] private float jumpSlamBoxWidth = 0.5f;
        [SerializeField] private float jumpSlamBoxHeight = 0.5f;
        [SerializeField] private float jumpSlamBoxPositionOffset = 0.5f;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _basicPlayerMovement = GetComponent<BasicPlayerMovement>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && _meleeAttackCooldownTimer > meleeAttackCooldown && _basicPlayerMovement.CanAttack())
                MeleeAttack();

            if(InAir())
                if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.S) ||
                    Input.GetButton("Fire1") && Input.GetKeyDown(KeyCode.S))
                {
                    _jumpSlamed = true;
                    _rigidbody.AddForce(Vector2.down * jumpSlamForce, ForceMode2D.Impulse);
                }

            if (Input.GetButtonDown("Fire2") && _rangedAttackCooldownTimer > rangedAttackCooldown && _basicPlayerMovement.CanAttack())
                RangedAttack();

            if ((CollideWithEnemy() || !InAir()) && _jumpSlamed)
            {
                JumpSlam();
                _jumpSlamed = false;
            }
            
            _meleeAttackCooldownTimer += Time.deltaTime;
            _rangedAttackCooldownTimer += Time.deltaTime;
        }

        private void MeleeAttack()
        {
            _meleeAttackCooldownTimer = 0;
            //Player Attack animation goes here
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                //TODO: Add deal damage component
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

        private void RangedAttack()
        {
            _rangedAttackCooldownTimer = 0;
            projectilePrefabs[FindProjectile()].transform.position = firePoint.position;
            projectilePrefabs[FindProjectile()].GetComponent<SwordProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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

        private int FindProjectile()
        {
            for (int i = 0; i < projectilePrefabs.Length; i++)
            {
                if (!projectilePrefabs[i].activeInHierarchy)
                    return i;
            }

            return 0;
        }
    }
}


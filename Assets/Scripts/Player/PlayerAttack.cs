using UnityEngine;

//TODO: Delete Debug.Log-s before final build
//TODO: Create condition for attacking only if player "has" a sword
namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private static readonly int Property = Animator.StringToHash("ranged attack");

        [Header("Melee Attack")] [SerializeField]
        private Transform attackPoint;

        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private float meleeAttackCooldown;
        [SerializeField] private LayerMask enemyLayer;

        [Header("Ranged Attack")] [SerializeField]
        private float rangedAttackCooldown;

        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject[] projectilePrefabs;

        [Header("JumpSlam")] [SerializeField] private float jumpSlamForce = 10;
        [SerializeField] private LayerMask platformLayer;
        [SerializeField] private int damage;

        [SerializeField] private Transform jumpSlamPoint;
        [SerializeField] private float jumpSlamBoxWidth = 0.5f;
        [SerializeField] private float jumpSlamBoxHeight = 0.5f;
        [SerializeField] private float jumpSlamBoxPositionOffset = 0.5f;
        private Animator _animator;
        private BasicPlayerMovement _basicPlayerMovement;
        private BoxCollider2D _boxCollider;

        private bool _jumpSlamed;
        private float _meleeAttackCooldownTimer = Mathf.Infinity;
        private int _meleeAttackIndex = 1;
        private float _rangedAttackCooldownTimer = Mathf.Infinity;
        private Rigidbody2D _rigidbody;

        public Health.Health health;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _basicPlayerMovement = GetComponent<BasicPlayerMovement>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && _meleeAttackCooldownTimer > meleeAttackCooldown &&
                _basicPlayerMovement.CanAttack())
                MeleeAttack();

            if (InAir())
                if ((Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.S)) ||
                    (Input.GetButton("Fire1") && Input.GetKeyDown(KeyCode.S)))
                {
                    _jumpSlamed = true;
                    _rigidbody.AddForce(Vector2.down * jumpSlamForce, ForceMode2D.Impulse);
                }

            if (Input.GetButtonDown("Fire2") && _rangedAttackCooldownTimer > rangedAttackCooldown &&
                _basicPlayerMovement.CanAttack())
                RangedAttack();

            if ((CollideWithEnemy() || !InAir()) && _jumpSlamed)
            {
                JumpSlam();
                _jumpSlamed = false;
            }

            _meleeAttackCooldownTimer += Time.deltaTime;
            _rangedAttackCooldownTimer += Time.deltaTime;
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null && jumpSlamPoint == null) return;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            Gizmos.DrawWireCube(jumpSlamPoint.position + new Vector3(0, jumpSlamBoxPositionOffset, 0),
                new Vector3(jumpSlamBoxWidth, jumpSlamBoxHeight, 0));
        }

        private void MeleeAttack()
        {
            _meleeAttackCooldownTimer = 0;
            _animator.SetTrigger("melee attack" + _meleeAttackIndex);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            Collider2D[] hitPlatforms = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, platformLayer);

            foreach (var enemy in hitEnemies)
                if(enemy.isTrigger)
                    enemy.GetComponent<Health.Health>().TakeDamage(damage, transform.position);


            if (_meleeAttackIndex == 3) _meleeAttackIndex = 1;
            else _meleeAttackIndex++;

            foreach (var platform in hitPlatforms)
                platform.GetComponent<Health.Health>().TakeDamage(damage);
        }

        private void JumpSlam()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
                jumpSlamPoint.position + new Vector3(0, jumpSlamBoxPositionOffset, 0),
                new Vector3(jumpSlamBoxWidth, jumpSlamBoxHeight, 0), 0f, enemyLayer);
            foreach (var enemy in hitEnemies)
                enemy.GetComponent<Health.Health>().TakeDamage(damage, transform.position);
        }

        private void RangedAttack()
        {
            _rangedAttackCooldownTimer = 0;
            _animator.SetTrigger(Property);
            projectilePrefabs[FindProjectile()].transform.position = firePoint.position;
            projectilePrefabs[FindProjectile()].GetComponent<SwordProjectile>()
                .SetDirection(Mathf.Sign(transform.localScale.x));
        }

        private bool InAir()
        {
            var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down,
                0.1f, LayerMask.GetMask("Ground"));
            return raycastHit.collider == null;
        }

        private bool CollideWithEnemy()
        {
            var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down,
                0.1f, LayerMask.GetMask("Enemy"));
            return raycastHit.collider != null;
        }

        private int FindProjectile()
        {
            for (var i = 0; i < projectilePrefabs.Length; i++)
                if (!projectilePrefabs[i].activeInHierarchy)
                    return i;

            return 0;
        }
    }
}
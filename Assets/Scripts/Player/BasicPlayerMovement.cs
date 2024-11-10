using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class BasicPlayerMovement : MonoBehaviour
    {
        private static readonly int Run = Animator.StringToHash("run");
        private static readonly int Grounded = Animator.StringToHash("grounded");
        private static readonly int Falling = Animator.StringToHash("falling");
        private static readonly int Jump1 = Animator.StringToHash("jump");

        [Header("Movement Parameters")]
        [SerializeField] private float speed;

        [SerializeField] private float jumpForce;
        [SerializeField] private int extraJumps = 1;
        [SerializeField] private float extraJumpForceModifier = 0.5f;

        [Header("Coyote Time")]
        [SerializeField] private float coyoteTime;

        [Header("Wall Sliding Parameters")]
        [SerializeField] private float wallSlideSpeed;
        [SerializeField] private float wallJumpingTime;
        [SerializeField] private float wallJumpingDuration;
        [SerializeField] private Vector2 wallJumpingPower = new(8, 16);

        [Header("Wall Jumping Parameters")]
        private bool _isWallJumping;
        private bool _isWallSliding;
        private int _jumpCount;

        [Header("Jump Parameters")]
        private bool _performJump;

        private Rigidbody2D _rigidbody;

        private float _wallJumpCooldown;
        private float _wallJumpingCounter;
        private float _wallJumpingDirection;
        private float _xInput;
        private float _prevY;
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private float _coyoteCooldown;
        private bool _isFacingRight = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.freezeRotation = true;
            GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _xInput = Input.GetAxisRaw("Horizontal");
            //Jump
            if (Input.GetButtonDown("Jump") && !OnWall()) _performJump = true;
            if (IsGrounded() || OnEnemy())
            {
                _coyoteCooldown = coyoteTime; //Resetting coyote time cooldown
                _jumpCount = extraJumps;
                _animator.ResetTrigger("jump");
            }
            else
            {
                _coyoteCooldown -= Time.fixedDeltaTime;
            }

            if (!_isWallJumping) FlipSprite();
            WallJump();
            WallSlide();
            _animator.SetBool(Run, _xInput != 0);
            _animator.SetBool(Grounded, IsGrounded() && IsFalling());
            _animator.SetBool(Falling, IsFalling() && !IsGrounded());
        }

        private void FixedUpdate()
        {
            if (!_isWallJumping) Move();

            if (_performJump) Jump();
        }

        private void Jump()
        {
            if (_coyoteCooldown <= 0 && !OnWall() && _jumpCount <= 0) return;
            _animator.SetTrigger(Jump1);
            if (IsGrounded() || OnEnemy()) //TODO: think about jump of enemies head - can double jump? coyote time?
            {
                _performJump = false;
                _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                if (_coyoteCooldown > 0)
                {
                    _performJump = false;
                    _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
                else
                {
                    if (_jumpCount > 0)
                    {
                        _performJump = false;
                        _rigidbody.velocity = Vector2.zero;
                        _rigidbody.AddForce(new Vector2(0, jumpForce * extraJumpForceModifier), ForceMode2D.Impulse);
                        _jumpCount--;
                    }
                }
            }

            _coyoteCooldown = 0;
        }

        private void Move()
        {
            _rigidbody.velocity = new Vector2(_xInput * speed, _rigidbody.velocity.y);
        }

        private void FlipSprite()
        {
            if ((_isFacingRight && _xInput < 0f) || (!_isFacingRight && _xInput > 0f))
            {
                _isFacingRight = !_isFacingRight;
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }

        private void WallSlide()
        {
            if (OnWall() && !IsGrounded() && _xInput != 0f)
            {
                _isWallSliding = true;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,
                    Mathf.Clamp(_rigidbody.velocity.y, -wallSlideSpeed, float.MaxValue));
            }
            else
            {
                _isWallSliding = false;
            }
        }

        private void WallJump()
        {
            if (_isWallSliding)
            {
                _isWallJumping = false;
                _wallJumpingDirection = -transform.localScale.x;
                _wallJumpingCounter = wallJumpingTime;

                CancelInvoke(nameof(StopWallJumping));
            }
            else
            {
                _wallJumpingCounter -= Time.fixedDeltaTime;
            }

            if (Input.GetButtonDown("Jump") && _wallJumpingCounter > 0)
            {
                _isWallJumping = true;
                _rigidbody.velocity = new Vector2(_wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                _wallJumpingCounter = 0;


                if (!Mathf.Approximately(transform.localScale.x, _wallJumpingDirection))
                {
                    _isFacingRight = !_isFacingRight;
                    var localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }

        private void StopWallJumping()
        {
            _isWallJumping = false;
        }

        private bool IsGrounded()
        {
            var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down,
                0.1f, LayerMask.GetMask("Ground", "Platform"));

            return raycastHit.collider != null;
        }

        private bool OnWall()
        {
            var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
                new Vector2(transform.localScale.x, 0), 0.1f, LayerMask.GetMask("Wall"));
            return raycastHit.collider != null;
        }

        private bool OnEnemy()
        {
            var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down,
                0.1f, LayerMask.GetMask("Enemy"));
            return raycastHit.collider != null;
        }

        public bool CanAttack()
        {
            return !OnWall() && !InAir(); //TODO: Possibly add other conditions for attack
        }

        private bool IsFalling()
        {
            var currY = transform.position.y;

            var travel = currY - _prevY;
            _prevY = currY;

            return travel < 0;
        }

        private bool InAir()
        {
            var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down,
                0.1f, LayerMask.GetMask("Ground"));
            return raycastHit.collider == null;
        }
    }
}
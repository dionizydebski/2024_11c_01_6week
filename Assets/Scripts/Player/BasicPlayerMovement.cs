using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    //TODO: Change wall slide / wall jump

    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class BasicPlayerMovement : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private BoxCollider2D _boxCollider;

        private float _xInput;
        [Header("Movement Parameters")]
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        
        [Header("Jump Parameters")]
        private bool _performJump;
        [SerializeField] private int extraJumps = 1;
        private int _jumpCount;

        [Header("Coyote Time")]
        [SerializeField] private float coyoteTime;
        private float _coyoteCooldown;

        private float _wallJumpCooldown;
        private float _mainGravityScale;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.freezeRotation = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _mainGravityScale = _rigidbody.gravityScale;
        }

        private void Update()
        {
            _xInput = Input.GetAxisRaw("Horizontal");
            //Jump
            if (Input.GetButtonDown("Jump"))
            {
                _performJump = true;
            }
            if (IsGrounded())
            {
                _coyoteCooldown = coyoteTime; //Reseting coyote time cooldown
                _jumpCount = extraJumps;
            }
            else
                _coyoteCooldown -= Time.fixedDeltaTime;

        }

        private void FixedUpdate()
        {
            Move();

            if (_performJump)
            {
                Jump();
            }

            FlipSprite();
        }

        private void Jump()
        {
            if (_coyoteCooldown <= 0 && !OnWall() && _jumpCount <= 0) return;
            //Jump animation goes here
            if (IsGrounded())
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
                        _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                        _jumpCount--;
                    }
                }
            }
            _coyoteCooldown = 0;
        }

        private void Move()
        {
            //Move animation goes here
            _rigidbody.velocity = new Vector2(_xInput*speed, _rigidbody.velocity.y);
        }

        private void FlipSprite()
        {
            if (_xInput > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if(_xInput < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        private bool IsGrounded()
        {
            RaycastHit2D reycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,0, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
            return reycastHit.collider != null;
        }
        private bool OnWall()
        {
            RaycastHit2D reycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,0, new Vector2(transform.localScale.x, 0), 0.1f, LayerMask.GetMask("Wall"));
            return reycastHit.collider != null;
        }
    }
}


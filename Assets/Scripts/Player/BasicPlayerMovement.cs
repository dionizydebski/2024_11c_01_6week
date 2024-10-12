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
        private float _xInput;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        private bool _performJump;
        private int _jumpCount;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private BoxCollider2D _boxCollider;
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

            //Double Jump
            if (_wallJumpCooldown > 0.2f)
            {
                if (Input.GetButtonDown("Jump") && _jumpCount > 0)
                {
                    _performJump = true;
                }

                if (OnWall() && !IsGrounded())
                {
                    _rigidbody.gravityScale = 0;
                    _rigidbody.velocity = Vector2.zero;
                }
                else
                {
                    _rigidbody.gravityScale = _mainGravityScale;
                }
            }
            else
            {
                _wallJumpCooldown += Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {

            if (IsGrounded())
            {
                _jumpCount = 2;
            }
            else if (OnWall())
            {
                _jumpCount = 1;
            }

            Move();

            Jump();

            FlipSprite();
        }

        private void Jump()
        {
            //Jump animation goes here
            if (_performJump)
            {
                _performJump = false;
                _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _jumpCount -= 1;
            }
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


using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    //TODO: Fix clipping to the ground while falling and pressing direction key
    //TODO: Test double jump force with level ready

    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class BasicPlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _xInput;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        private bool _isGrounded;
        private bool _performJump;
        private int _jumpCount;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.freezeRotation = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _xInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && _jumpCount > 0)
            {
                _performJump = true;
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_xInput * speed, _rigidbody.velocity.y);

            if (_performJump)
            {
                _performJump = false;
                _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _jumpCount -= 1;
            }

            FlipSprite();
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _jumpCount = 2;
            _animator.SetBool("IsGrounded", _isGrounded);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _animator.SetBool("IsGrounded", _isGrounded);
        }
    }
}


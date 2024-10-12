using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class BasicPlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _xInput;
        [FormerlySerializedAs("_speed")] [SerializeField] private float speed;
        private bool _isTurnedRight = true;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _xInput = Input.GetAxisRaw("Horizontal");
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_xInput * speed, _rigidbody.velocity.y);
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
    }
}


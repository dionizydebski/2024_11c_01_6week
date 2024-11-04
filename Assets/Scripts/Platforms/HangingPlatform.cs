using UnityEngine;

namespace Platforms
{
    public class HangingPlatform : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private Transform fallPoint;
        [SerializeField] private float speed;
        private Vector2 _targetPosition;
        private bool _isFalling;
        private bool _fell;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate() //BUG: For now max speed is 15. More than that and platform bugs out while reaching fall point.
        {
            if (_isFalling && !_fell)
            {
                Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
                _rb.MovePosition((Vector2)transform.position + direction * (speed * Time.fixedDeltaTime));
                if (Vector2.Distance(transform.position, _targetPosition) < 0.1f || transform.position.y < _targetPosition.y)
                {
                    Debug.Log("Collapsed");
                    _isFalling = false;
                    _fell = true;
                    _rb.velocity = Vector2.zero;
                }
            }
        }

        public void Fall()
        {
            _targetPosition = fallPoint.position;
            _isFalling = true;
        }
    }
}


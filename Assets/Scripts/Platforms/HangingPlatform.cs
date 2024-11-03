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

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate() //BUG: For now max speed is 15. More than that and platform bugs out while reaching fall point.
        {
            if (_isFalling)
            {
                Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
                _rb.MovePosition((Vector2)transform.position + direction * (speed * Time.fixedDeltaTime));
                if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
                {
                    Debug.Log("Collapsed");
                    _isFalling = false;
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


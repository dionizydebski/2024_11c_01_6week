using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class SwordProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float _direction;
        private bool _hit;
        [SerializeField] private float lifeTime;

        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(_hit) return;
            _rigidbody.velocity = new Vector2(speed, 0);

            lifeTime += Time.deltaTime;
            if (lifeTime > 5) gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _hit = true;
            _boxCollider.enabled = false;
            //Play hit animation
        }

        public void SetDirection(float direction)
        {
            lifeTime = 0;
            _direction = direction;
            gameObject.SetActive(true);
            _hit = false;
            _boxCollider.enabled = true;

            float localScaleX = transform.localScale.x;
            if(Mathf.Sign(localScaleX) != direction)
                localScaleX = -localScaleX;

            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}


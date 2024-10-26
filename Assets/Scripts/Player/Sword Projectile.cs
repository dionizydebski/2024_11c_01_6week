using UnityEngine;

namespace Player
{
    public class SwordProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float projectileDamage;
        private float _direction;
        private bool _hit;
        [SerializeField] private float lifeTime;
        [SerializeField] private string layerName;

        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(_hit) return;
            transform.Translate(speed * Time.deltaTime * _direction, 0, 0);

            lifeTime += Time.deltaTime;
            if (lifeTime > 5) gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _hit = true;
            _boxCollider.enabled = false;
            if (other.gameObject.layer == LayerMask.NameToLayer(layerName)) {
                Debug.Log("enemy hit with ranged attack");
                other.GetComponent<Health.Health>().TakeDamage(projectileDamage, transform.position);
                //Destroy projectile animation
            }
            Deactivate();

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


using UnityEngine;

namespace Player
{
    public class SwordProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float projectileDamage;
        [SerializeField] private float lifeTime;
        [SerializeField] private string layerName;

        private BoxCollider2D _boxCollider;
        private float _direction;
        private bool _hit;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (_hit) return;
            transform.Translate(speed * Time.deltaTime * _direction, 0, 0);

            lifeTime += Time.deltaTime;
            if (lifeTime > 5) gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _hit = true;
            _boxCollider.enabled = false;
            if (other.gameObject.layer == LayerMask.NameToLayer(layerName) && other.isTrigger)
            {
                other.GetComponent<Health.Health>().TakeDamage(projectileDamage, transform.position); //TODO: Add rigid body to enemy to fix knock back effect
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

            var localScaleX = transform.localScale.x;
            if (!Mathf.Approximately(Mathf.Sign(localScaleX), direction))
                localScaleX = -localScaleX;

            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
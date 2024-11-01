using UnityEngine;

namespace Player
{
    public class ItemDrop : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float dropForce;
        private float _lifeTime;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        }

        private void Update()
        {
            _lifeTime += Time.deltaTime;
            if (_lifeTime > 2) Destroy(gameObject);
        }
    }
}

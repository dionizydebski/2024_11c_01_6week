using System.Collections;
using UnityEngine;

namespace Platforms
{
    public class CollapsingPlatform : MonoBehaviour
    {
        private float _collapseDelay = 1f;
        private float _respawnTime = 3f;
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        
        private bool _isCollapsing;
        
        private Collider2D _platformCollider;
        private Collider2D _triggerCollider;
        private Renderer _platformRenderer;

        [SerializeField] private Rigidbody2D rb;
        
        private void Start()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            
            _platformCollider = GetComponent<Collider2D>();
            _triggerCollider = GetComponents<Collider2D>()[1];
            _platformRenderer = GetComponent<Renderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isCollapsing && other.CompareTag("Player"))
            {
                StartCoroutine(Collapse());
            }
        }

        private IEnumerator Collapse()
        {
            _isCollapsing = true;
            yield return new WaitForSeconds(_collapseDelay);

            // Wyłącz platformę (ukryj i dezaktywuj collider)
            _platformRenderer.enabled = false;
            _platformCollider.enabled = false;
            _triggerCollider.enabled = false;
            
            yield return new WaitForSeconds(_respawnTime);
            
            RespawnPlatform();
        }
        
        private void RespawnPlatform()
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            
            _platformRenderer.enabled = true;
            _platformCollider.enabled = true;
            _triggerCollider.enabled = true;

            _isCollapsing = false;
        }

    }
}

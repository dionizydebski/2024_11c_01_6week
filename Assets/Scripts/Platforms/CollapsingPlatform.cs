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
        private Renderer _platformRenderer;

        [SerializeField] private Rigidbody2D rb;
        
        private void Start()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            
            _platformCollider = GetComponent<Collider2D>();
            _platformRenderer = GetComponent<Renderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isCollapsing && collision.gameObject.CompareTag("Player"))
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
            
            yield return new WaitForSeconds(_respawnTime);
            
            RespawnPlatform();
        }
        
        private void RespawnPlatform()
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            
            _platformRenderer.enabled = true;
            _platformCollider.enabled = true;

            _isCollapsing = false;
        }

    }
}

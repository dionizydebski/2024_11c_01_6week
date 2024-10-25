using System.Collections;
using UnityEngine;

namespace Platforms
{
    public class CollapsingPlatform : MonoBehaviour
    {
        private float collapseDelay = 1f;
        private float respawnTime = 3f;
        
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private Collider2D platformCollider;
        private Renderer platformRenderer;

        [SerializeField] private Rigidbody2D rb;
        
        private void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            
            platformCollider = GetComponent<Collider2D>();
            platformRenderer = GetComponent<Renderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Collapse());
            }
        }

        private IEnumerator Collapse()
        {
            yield return new WaitForSeconds(collapseDelay);

            // Wyłącz platformę (ukryj i dezaktywuj collider)
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
            
            yield return new WaitForSeconds(respawnTime);
            
            RespawnPlatform();
        }
        
        private void RespawnPlatform()
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            
            platformRenderer.enabled = true;
            platformCollider.enabled = true;
        }

    }
}

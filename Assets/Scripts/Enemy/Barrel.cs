using System.Collections;
using UnityEngine;

namespace enemy
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private Transform posA;
        [SerializeField] private Transform posB;
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float respawnTime;
        [SerializeField] private int damage;
        [SerializeField] private Health playerHealth;

        private Quaternion initialRotation;
        private Collider2D platformCollider;
        private Renderer platformRenderer;

        private void Start()
        {
            initialRotation = transform.rotation;

            platformCollider = GetComponent<Collider2D>();
            platformRenderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, posB.position) < 0.1f)
            {
                StartCoroutine(Die());
            }

            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, posB.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerHealth.TakeDamage(damage);
            }
        }

        private IEnumerator Die()
        {
            platformCollider.enabled = false;
            platformRenderer.enabled = false;

            yield return new WaitForSeconds(respawnTime);

            RespawnPlatform();
        }

        private void RespawnPlatform()
        {
            transform.position = posA.position;
            transform.rotation = initialRotation;

            platformRenderer.enabled = true;
            platformCollider.enabled = true;
        }
    }
}
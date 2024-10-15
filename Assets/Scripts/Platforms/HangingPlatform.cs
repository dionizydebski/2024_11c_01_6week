using UnityEngine;

namespace Platforms
{
    public class HangingPlatform : MonoBehaviour
    {
        [SerializeField] private float breakForce = 10f; 
        [SerializeField] private float downwardVelocityThreshold = -2f; 
        [SerializeField] private Rigidbody2D rb;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (playerRb != null && playerRb.velocity.y < downwardVelocityThreshold)
                {
                    if (collision.relativeVelocity.magnitude > breakForce)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }
                }
            }
        }
    }
}


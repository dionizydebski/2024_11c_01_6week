using UnityEngine;

namespace Platforms
{
    public class BreakablePlatform : MonoBehaviour
    {
        [SerializeField] private float breakForce = 10f; 
        [SerializeField] private float downwardVelocityThreshold = -2f; 

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null && playerRb.velocity.y < downwardVelocityThreshold)
                {
                    if (collision.relativeVelocity.magnitude > breakForce)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}


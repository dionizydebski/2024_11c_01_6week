using System.Collections;
using UnityEngine;

namespace Platforms
{
    public class CollapsingPlatform : MonoBehaviour
    {
        private float collapsDelay = 1f;
        private float destroyDelay = 2f;

        [SerializeField] private Rigidbody2D rb;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Collaps());
            }
        }

        private IEnumerator Collaps()
        {
            yield return new WaitForSeconds(collapsDelay);
            rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, destroyDelay);
        }

    }
}

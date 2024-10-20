using UnityEngine;

namespace Platforms
{
    public class MovingPlatformScript : MonoBehaviour
    {
        public Transform posA; // Punkt startowy
        public Transform posB; // Punkt końcowy
        public float speed; // Prędkość platformy
        private Vector3 target; 

        private void Start()
        {
            target = posB.position; 
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                target = (target == posA.position) ? posB.position : posA.position;
            }
        
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.SetParent(this.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (this.gameObject.activeInHierarchy) // Sprawdzenie, czy platforma jest aktywna
                {
                    collision.transform.SetParent(null);
                }
            }
        }
    }
}




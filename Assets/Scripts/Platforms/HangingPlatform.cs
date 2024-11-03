using UnityEngine;

namespace Platforms
{
    public class HangingPlatform : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void Collapse ()
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}


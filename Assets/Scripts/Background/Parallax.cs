using System;
using UnityEngine;

namespace Background
{
    public class Parallax : MonoBehaviour
    {
        Material mat;
        private float distance;

        [Range(0f, 0.5f)] 
        public float speed = 0.2f;

        private void Start()
        {
            mat = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            distance += Time.deltaTime * speed;
            mat.SetTextureOffset("_MainTex_", Vector2.right * distance);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private TilemapCollider2D tilemapCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] public Health.Health playerHealth;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

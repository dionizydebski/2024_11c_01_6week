using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private LayerMask _playerLayer;
    private float _cooldownTimer = Mathf.Infinity;
    private Health playerHealth;

    // Update is called once per frame
    void Update()
    {
        _attackCooldown += Time.deltaTime;
        
        if (PlayerInSight())
        {
            if (_cooldownTimer >= _attackCooldown)
            {
                _cooldownTimer = 0;
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider2D.bounds.center + transform.right * _range * transform.localScale.x, _boxCollider2D.bounds.size, 0, Vector2.left,
            0, _playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider2D.bounds.center + transform.right * _range * transform.localScale.x, _boxCollider2D.bounds.size);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.Damage(_damage);
        }
    }
}

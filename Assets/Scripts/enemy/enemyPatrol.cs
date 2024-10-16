using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 _initScale;
    private bool _movingLeft;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float _idleTimer;

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        anim.SetBool("moving", true);
        
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction,
            _initScale.y, _initScale.z);
        
        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void Awake()
    {
        _initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
                
            }
            
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
                
            }
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        _idleTimer += Time.deltaTime;

        if (_idleTimer > idleDuration)
        {
            _movingLeft = !_movingLeft;
        }
    }
}

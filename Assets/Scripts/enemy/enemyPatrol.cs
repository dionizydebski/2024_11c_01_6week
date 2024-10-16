using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;

    private void MoveInDirection(int _direction)
    {
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
    
    
    private Rigidbody2D _rb;
    private Transform _currentPoint;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = _currentPoint.position - transform.position;
        if (_currentPoint == pointB.transform)
        {
            _rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            _rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, _currentPoint.position) < 1.5f && _currentPoint == pointB.transform)
        {
            _currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, _currentPoint.position) < 1.5f && _currentPoint == pointA.transform)
        {
            _currentPoint = pointB.transform;
        }
    }
}

using System;
using UnityEngine;

namespace Pirate_Ship
{
    public class PirateShipMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        private bool _move;
        private bool _canMove = true;
        private Vector2 _targetVector;

        private void Start()
        {
            _targetVector = target.position;
        }

        private void FixedUpdate()
        {
            if (_move && _canMove)
            {
                if (Vector2.Distance(transform.position, _targetVector) < 0.1f)
                {
                    _move = false;
                    _canMove = false;
                    GameObject player = gameObject.transform.GetChild(3).gameObject;
                    if (gameObject.activeInHierarchy)
                    {
                        player.transform.SetParent(null);
                    }
                }
                else
                {
                    Movement();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(transform);
            }
            _move = true;
        }

        private void Movement()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (gameObject.activeInHierarchy)
                {
                    other.gameObject.transform.SetParent(null);
                }
            }
        }
    }
}

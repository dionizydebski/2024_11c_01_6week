using System;
using System.Collections;
using UnityEngine;

namespace Pirate_Ship
{
    public class PirateShipMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        private Animator _animator;
        private bool _move;
        private bool _canMove = true;
        private Vector2 _targetVector;


        private void Start()
        {
            _targetVector = target.position;
            _animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (_move && _canMove)
            {
                if (Vector2.Distance(transform.position, _targetVector) < 0.1f)
                { 
                    _animator.SetBool("Sail", false);
                    _move = false;
                    _canMove = false;
                    GameObject player = gameObject.transform.GetChild(2).gameObject;
                    if (gameObject.activeInHierarchy)
                    {
                        player.transform.SetParent(null);
                    }
                    _animator.SetBool("Wind", false);
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
            StartCoroutine(Wait(1f));
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

        private IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            _animator.SetBool("Wind", true);
            _animator.SetBool("Sail", true);
            yield return new WaitForSeconds(time/2);
            _move = true;
        }
    }
}

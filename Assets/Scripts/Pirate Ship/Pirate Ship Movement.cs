using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Pirate_Ship
{
    public class PirateShipMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject button;
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
                    _animator.SetBool("Wind", false);
                    button.SetActive(true);
                }
                else
                {
                    Movement();
                }
            }
        }

        private void Movement()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        public void SetMove(bool value)
        {
            _move = value;
        }

        public bool GetMove()
        {
            return _move;
        }

        public void SetAnimatorBool(string name, bool value)
        {
            if (HasParameter(name, _animator))
            {
                _animator.SetBool(name, value);
            }
        }

        private static bool HasParameter( string paramName, Animator animator )
        {
            foreach( AnimatorControllerParameter param in animator.parameters )
            {
                if( param.name == paramName )
                    return true;
            }
            return false;
        }
    }
}

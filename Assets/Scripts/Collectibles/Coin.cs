using System;
using Managers;
using UnityEngine;
using Player;
using UnityEngine.Events;

namespace Collectibles
{
    public class Coin : Collectible
    {
        public UnityEvent coinCollect;
        private Animator _animator;

        private void Start()
        {
            coinCollect.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().IncrementCoinsScore);
            coinCollect.AddListener(GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateCoinsScore);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void Collect()
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddCoin();
                coinCollect.Invoke();
                Debug.Log("Setting isPickedUp to true");
                _animator.SetBool("isPickedUp", true);
                //Destroy(gameObject);
                Invoke(nameof(DestroyCoin), 0.5f);
            }
            else
            {
                Debug.LogError("PlayerInventory not found! Coin collection failed.");
            }
        }
        
        private void DestroyCoin()
        {
            Destroy(gameObject);
        } 
    }
}
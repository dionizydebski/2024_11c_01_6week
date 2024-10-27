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

        private void Start()
        {
            coinCollect.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().IncrementCoinsScore);
            coinCollect.AddListener(GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateCoinsScore);

        }

        protected override void Collect()
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddCoin();
            }
            
            coinCollect.Invoke();
            Destroy(gameObject);
        }
    }
}
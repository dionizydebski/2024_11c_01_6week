using UnityEngine;
using Player;

namespace Collectibles
{
    public class Coin : Collectible
    {
        protected override void Collect()
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddCoin();
            }
            Destroy(gameObject);
        }
    }
}
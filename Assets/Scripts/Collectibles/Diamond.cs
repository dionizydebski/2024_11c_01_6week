using UnityEngine;
using Player;

namespace Collectibles
{
    public class Diamond : Collectible
    {
        protected override void Collect()
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddDiamond();
            }
        }
    }
}
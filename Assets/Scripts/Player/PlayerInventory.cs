using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int coins = 0;
        [SerializeField] private int diamonds = 0;

        public void AddCoin()
        {
            coins++;
            Debug.Log("Coins collected: " + coins);
        }

        public void AddDiamond()
        {
            diamonds++;
            Debug.Log("Diamonds collected: " + diamonds);
        }

        public int GetCoins()
        {
            return coins;
        }

        public int GetDiamonds()
        {
            return diamonds;
        }
    }
}
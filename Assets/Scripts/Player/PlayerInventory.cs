using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private int _coins = 0;
        private int _diamonds = 0;

        public void AddCoin()
        {
            _coins++;
            Debug.Log("Coins collected: " + _coins);
        }

        public void AddDiamond()
        {
            _diamonds++;
            Debug.Log("Diamonds collected: " + _diamonds);
        }

        public int GetCoins()
        {
            return _coins;
        }

        public int GetDiamonds()
        {
            return _diamonds;
        }
    }
}
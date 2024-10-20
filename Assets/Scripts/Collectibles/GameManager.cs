using UnityEngine;

namespace Collectibles
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private int coins = 0;
        [SerializeField] private int diamonds = 0;
        [SerializeField] private int health = 100;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
    
        public int GetCoins()
        {
            return coins;
        }

        public void AddCoin()
        {
            coins++;
        }

        public int GetDiamonds()
        {
            return diamonds;
        }

        public void AddDiamond()
        {
            diamonds++;
        }

        public int GetHealth()
        {
            return health;
        }

        public void AddHealth(int amount)
        {
            health = Mathf.Min(health + amount, 100); // Max wartość zdrowia to 100
        }
    }
}
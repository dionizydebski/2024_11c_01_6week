using UnityEngine;

namespace Collectibles
{
    public class Collectibles2D : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                GameManager.Instance.AddCoin();
                Debug.Log("Coins collected: " + GameManager.Instance.GetCoins());
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Diamond"))
            {
                GameManager.Instance.AddDiamond();
                Debug.Log("Diamonds collected: " + GameManager.Instance.GetDiamonds());
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("HealthPotion"))
            {
                GameManager.Instance.AddHealth(20);
                Debug.Log("Health: " + GameManager.Instance.GetHealth());
                Destroy(other.gameObject);
            }
        }
    }
}

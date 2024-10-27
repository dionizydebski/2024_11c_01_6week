using UnityEngine;

namespace Managers
{
   public class GameController : MonoBehaviour
   {
      public int coinsScore { get; private set; }

      public void IncrementCoinsScore()
      {
         coinsScore++;
      } 
   }
}

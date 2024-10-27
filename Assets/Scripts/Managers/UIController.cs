using System;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsScoreText;

        private GameController gameController;

        private void Start()
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        public void UpdateCoinsScore()
        {
            coinsScoreText.text = gameController.coinsScore.ToString();
        }
    }
}

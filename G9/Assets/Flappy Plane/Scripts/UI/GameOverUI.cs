using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace G9.MiniGame.FlappyPlane.UI
{
    public class GameOverUI : MonoBehaviour
    {

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI BestScoreText;

        [SerializeField]
        private Canvas _canvas;

        public void Show() => _canvas.gameObject.SetActive(true);
        public void Hide() => _canvas.gameObject.SetActive(false);


        public void UpdateScore(int score)
        {
            int bestScore = PlayerPrefs.GetInt("FlappyPlane_BestScore");
            if (score > bestScore)
                bestScore = score;

            scoreText.text = score.ToString();
            BestScoreText.text = bestScore.ToString();
        }

    }
}

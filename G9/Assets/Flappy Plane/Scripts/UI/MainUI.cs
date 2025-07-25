using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace G9.MiniGame.FlappyPlane.UI
{
    public class MainUI : MonoBehaviour
    {
        // 게임 시작 시 나오는 UI(씬 전환하자 마자 나오는 씬
        public TextMeshProUGUI scoreText;

        [SerializeField]
        private Canvas _canvas;

        public void Show() => _canvas.gameObject.SetActive(true);
        public void Hide() => _canvas.gameObject.SetActive(false);

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

    }
}

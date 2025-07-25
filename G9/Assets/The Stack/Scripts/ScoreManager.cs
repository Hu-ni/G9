using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace G9.MiniGame.TheStack
{
    public class ScoreManager
    {
        public const string BestScoreKey = "TheStack_BestScore";
        public const string BestComboKey = "TheStack_BestCombo";

        public int BestScore { get; private set; }
        public int BestCombo { get; private set; }

        public ScoreManager() 
        {
            // 저장된 데이터 불러오기 변경하기
            BestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            BestCombo = PlayerPrefs.GetInt(BestComboKey, 0);
        }

        // 최고 점수 갱신
        public void UpdateScore(int currentScore, int currentCombo)
        {
            if(BestScore < currentScore)
                BestScore = currentScore;
            if(BestCombo < currentCombo)
                BestCombo = currentCombo;

            PlayerPrefs.SetInt(BestScoreKey, BestScore);
            PlayerPrefs.SetInt(BestComboKey, BestCombo);
        }
    }
}

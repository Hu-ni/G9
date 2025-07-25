using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace G9.Game.UI
{
    public enum MiniGame
    { 
        FlappyPlane, TheStack
    }
    public class LeaderBoardEntry : MonoBehaviour
    {
        public TextMeshProUGUI LeaderText;
        public MiniGame game;

        private string[] games = new string[] { "FlappyPlane_", "TheStack_" };

        void Start()
        {

        }

        void Update()
        {

        }

        public void Setup()
        {
            string text = "";
            switch(game)
            {
                case MiniGame.FlappyPlane:
                    //int bestScore = PlayerPrefs.GetInt("FlappyPlane_BestScore");
                    //text = $"{bestScore}";  // 
                    
                    break;
                case MiniGame.TheStack:
                    break;
            }

            LeaderText.text = text;
        }
    }
}
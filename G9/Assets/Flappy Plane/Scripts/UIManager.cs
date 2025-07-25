using G9.MiniGame.FlappyPlane.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace G9.MiniGame.FlappyPlane
{
    public enum GameState { Start, Main, GameOver };

    public class UIManager : MonoBehaviour
    {
        public StartUI startUI;
        public GameOverUI gameOverUI;
        public MainUI mainUI;


        public void Start()
        {
        }

        public void ChangedState(GameState state)
        {
            switch(state)
            {
                case GameState.Start:
                    startUI.Show();
                    gameOverUI.Hide();
                    mainUI.Hide();
                    break;
                case GameState.Main:
                    startUI.Hide();
                    gameOverUI.Hide();
                    mainUI.Show();
                    break;
                case GameState.GameOver:
                    startUI.Hide();
                    gameOverUI.Show();
                    mainUI.Hide();
                    break;
            }
        }

        public void UpdateScore(int score)
        {
            mainUI.UpdateScore(score);
        }

        public void SetSecore(int score)
        {
            gameOverUI.UpdateScore(score);
        }
    }
}
using G9.MiniGame.FlappyPlane.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace G9.MiniGame.FlappyPlane
{
    public class GameManager : MonoBehaviour
    {
        static GameManager gameManager;

        public static GameManager Instance
        {
            get { return gameManager; }
        }

        static UIManager uiManager;

        public static UIManager UIManager
        {
            get { return uiManager; }
        }

        private int currentScore = 0;

        public bool isGameStarted = false;

        public const string BESTSCORE = "FlappyPlane_BestScore";


        private void Awake()
        {
            gameManager = this;
            uiManager = FindObjectOfType<UIManager>();
            Time.timeScale = 0.0f;
        }

        private void Start()
        {
            uiManager.UpdateScore(0);
            uiManager.ChangedState(GameState.Start);

        }

        public void GameOver()
        {
            //Debug.Log("Game Over");
            int bestScore = PlayerPrefs.GetInt(BESTSCORE, 0);
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt(BESTSCORE, bestScore);
            }
            uiManager.SetSecore(currentScore);
            uiManager.ChangedState(GameState.GameOver);

        }

        public void RestartGame()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);   //테스트용
            SceneManager.LoadScene("SampleScene");
        }

        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
            //Debug.Log("Score: " + currentScore);
        }

        public void GameStart()
        {
            isGameStarted = true;
            uiManager.ChangedState(GameState.Main);
            Time.timeScale = 1.0f;
        }
    }
}
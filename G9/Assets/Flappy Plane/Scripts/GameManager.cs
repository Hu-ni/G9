using G9.Const;
using G9.Game.DTO;
using G9.Game.LeaderBoard;
using G9.Game.Util;
using G9.MiniGame.FlappyPlane.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace G9.MiniGame.FlappyPlane
{
    public class GameManager : MonoBehaviour
    {
        private const string gameId = ConstValues.FlappyPlane;
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
            int bestScore = PlayerPrefs.GetInt(ConstValues.FlappyPlane_BestScore, 0);
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt(ConstValues.FlappyPlane_BestScore, bestScore);
            }
            uiManager.SetSecore(currentScore);
            SaveLeaderBoard();

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

        // 리더보드에 데이터 저장
        public void SaveLeaderBoard()
        {
            var entry = new LeaderBoardEntry
            {
                playerName = "Test",
                score = currentScore,
                //playTime = currentPlayTime,
                timestamp = System.DateTime.Now.ToString("s"),
                extraData = new Dictionary<string, string>
                {
                    
                }
            };

            // 기존 랭킹 읽기
            var leaderboard = LeaderboardFileUtil.LoadLeaderboard(gameId);

            // 기록 추가 + 정렬
            leaderboard.entries.Add(entry);
            leaderboard.entries = leaderboard.entries
                .OrderByDescending(e => e.score)
                .Take(100)
                .ToList();

            // 파일로 저장
            LeaderboardFileUtil.SaveLeaderBoard(gameId, leaderboard);
        }
    }
}
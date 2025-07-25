using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace G9.MiniGame.TheStack
{
    public enum UIState
    {
        Home,
        Game,
        Score,
    }

    public class UIManager : MonoBehaviour
    {
        static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                return instance;
            }
        }

        UIState currentState = UIState.Home;

        HomeUI homeUI = null;
        GameUI gameUI = null;
        ScoreUI scoreUI = null;
        TheStack theStack = null;

        GameManager gameManager = null;

        private void Awake()
        {
            instance = this;

            gameManager = FindObjectOfType<GameManager>();
            theStack = FindObjectOfType<TheStack>();
            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI?.Init(this);
            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);
            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);
            ChangeState(UIState.Home);
        }


        public void ChangeState(UIState state)
        {
            currentState = state;
            homeUI?.SetActive(currentState);
            gameUI?.SetActive(currentState);
            scoreUI?.SetActive(currentState);
        }

        public void OnClickStart()
        {
            gameManager.Restart();
            //theStack.Restart();
            ChangeState(UIState.Game);
        }


        public void OnClickExit()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void UpdateScore()
        {
            gameUI.SetUI(gameManager.Score, gameManager.Combo, gameManager.MaxCombo);
            //gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
        }

        public void SetScoreUI()
        {
            scoreUI.SetUI(gameManager.Score, gameManager.MaxCombo, gameManager.BestScore, gameManager.BestCombo);
            //scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo
            ChangeState(UIState.Score);
        }
    }

}


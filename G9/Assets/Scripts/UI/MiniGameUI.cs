using G9.Game.DTO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace G9.Game.UI
{
    public class MiniGameUI : MonoBehaviour
    {
        // 미니 게임 UI
        // ImageSlider 컴포넌트 등록

        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private ImageSlide slide;

        private string[] _miniGamesceneName = new string[] {"Flappy Plane","The Stack"};

        public void OnClose()
        {
            _canvas.gameObject.SetActive(false);
        }

        public void OnStartMiniGame()
        {
            GameStateManager.PlayerPosition = Player.Instance.transform.position;
            GameStateManager.SlideIndex = slide.CurrentIndex;
            GameStateManager.HasSavedState = true;

            int index = slide.CurrentIndex;
            if (index >= 0 && index < _miniGamesceneName.Length && !slide.IsSliding)
                SceneManager.LoadScene(_miniGamesceneName[slide.CurrentIndex]);
        }
    }
}
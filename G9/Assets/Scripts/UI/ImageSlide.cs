using G9.Game.DTO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace G9.Game.UI
{
    public class ImageSlide : MonoBehaviour
    {
        // 이미지 슬라이드 갤러리
        // 좌우 버튼을 눌러 이미지 슬라이드
        private Vector2 _screenSize = new Vector2(1920, 1080);

        public Image rightArrow;
        public Image leftArrow;

        public Image[] Thumnails;

        private int currentIdx = 0;
        public int CurrentIndex => currentIdx;
        private int beforeIdx = -1;

        public float slideDuration = 0.5f;
        private bool isSliding = false;
        public bool IsSliding => isSliding;
        // Start is called before the first frame update
        void Start()
        {
            currentIdx = GameStateManager.HasSavedState ? GameStateManager.SlideIndex : 0;

            for (int i = 0; i < Thumnails.Length; i++)
            {
                Thumnails[i].transform.localPosition = new Vector3((i - currentIdx) * _screenSize.x, 0, 0);
            }

            UpdateArrowState();
        }

        public void OnClickRightArrow()
        {
            if (isSliding || currentIdx >= Thumnails.Length - 1) return;

            if (!leftArrow.gameObject.activeSelf)
                leftArrow.gameObject.SetActive(true);

            beforeIdx = currentIdx;
            currentIdx++;
            UpdateArrowState();

            StartCoroutine(SlideImage(-_screenSize.x));
        }

        public void OnClickLeftArrow()
        {
            if (isSliding || currentIdx <= 0) return;

            if (!rightArrow.gameObject.activeSelf)
                rightArrow.gameObject.SetActive(true);

            beforeIdx = currentIdx;
            currentIdx--;
            UpdateArrowState();

            StartCoroutine(SlideImage(_screenSize.x));
        }

        private IEnumerator SlideImage(float direction)
        {
            isSliding = true;
            float elapsed = 0f;
            Vector3 startBefore = Thumnails[beforeIdx].transform.localPosition;
            Vector3 endBefore = startBefore + new Vector3(direction, 0, 0);

            Vector3 startCurr = Thumnails[currentIdx].transform.localPosition;
            Vector3 endCurr = startCurr + new Vector3(direction, 0, 0);

            while (elapsed < slideDuration)
            {
                float t = elapsed / slideDuration;
                Thumnails[beforeIdx].transform.localPosition = Vector3.Lerp(startBefore, endBefore, t);
                Thumnails[currentIdx].transform.localPosition = Vector3.Lerp(startCurr, endCurr, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // 마지막 위치 정확히 정렬
            Thumnails[beforeIdx].transform.localPosition = endBefore;
            Thumnails[currentIdx].transform.localPosition = endCurr;
            isSliding = false;
        }

        private void UpdateArrowState()
        {
            if (currentIdx == 0)
                leftArrow.gameObject.SetActive(false);
            if (currentIdx >= Thumnails.Length - 1)
                rightArrow.gameObject.SetActive(false);
        }
    }
}
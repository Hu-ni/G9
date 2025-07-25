using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace G9.MiniGame.FlappyPlane.UI
{
    public class StartUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        public void Show() => _canvas.gameObject.SetActive(true);
        public void Hide() => _canvas.gameObject.SetActive(false);
    }
}

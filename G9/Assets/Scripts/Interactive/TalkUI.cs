using G9.Game.Interactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace G9.Game.UI
{
    [RequireComponent(typeof(NPC))]
    public class TalkUI : MonoBehaviour, IInteractable
    {
        //NPC와 상호작용할 때 나오는 대화창
        //NPC에게 붙이는 컴포넌트
        [SerializeField]
        private NPC _npc;

        [SerializeField]
        private Canvas _canvas; // 대화 창

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI contentText;

        [TextArea]
        public string content;

        bool isTalking = false;
        public string GetPromptMessage()
        {
            throw new NotImplementedException();
        }

        // 원래 대화 데이터는 다른 곳에서 받아야 됨. 하지만 일단 입력 받는걸로.
        public void Interact()
        {
            isTalking = true;
            _canvas.gameObject.SetActive(true);
            nameText.text = _npc.Name;
            contentText.text = content;
            _npc.isInteractive = true;
        }


        void Update()
        {
            if (isTalking && Input.GetKeyDown(KeyCode.Space))
            {
                _canvas.gameObject.SetActive(false);
                _npc.isInteractive = false;
            }
        }

        public void Talk()
        {
            _canvas.gameObject.SetActive(true);
        }

    }
}

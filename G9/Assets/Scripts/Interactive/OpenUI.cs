using G9.Game.Interactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour, IInteractable
{
    // 상호작용 시 UI 활성화
    public Canvas canvas;

    [TextArea]
    public string Text;

    public string GetPromptMessage()
    {
        return Text;
    }

    public void Interact()
    {
        canvas.gameObject.SetActive(true);
    }
}

using G9.Game.Interactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public string Name;
    [TextArea]
    public string Text;


    public string GetPromptMessage()
    {
        return Text;
    }

    
}

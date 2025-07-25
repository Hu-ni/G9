using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorizer
{
    private Color prevColor;
    private Color nextColor;

    public void Reset()
    {
        prevColor = GetNextColor();
        nextColor = GetNextColor();
    }

    public Color GetNextColor()
    {
        float h = Random.Range(0f, 1f);        // Hue 전체 범위
        float s = Random.Range(0.4f, 0.7f);    // Saturation: 선명한 색
        float v = Random.Range(0.6f, 0.85f);   // Value(Brightness): 너무 어둡거나 밝지 않게

        return Color.HSVToRGB(h, s, v);
    }

    public void ApplyColor(GameObject go, int stackCount)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        Renderer rn = go.GetComponent<Renderer>();

        if (rn == null)
        {
            Debug.Log("Renderer is NULL!");
            return;
        }

        rn.material.color = applyColor;
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetNextColor();
        }
    }

}

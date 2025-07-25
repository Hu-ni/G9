using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineWrapper : MonoBehaviour
{
    public string blockTag = "Block"; // 블록에 공통된 태그 사용
    public float outlineScale = 1.05f;
    public float darknessFactor = 0.6f;

    void Start()
    {
        var blocks = GameObject.FindGameObjectsWithTag(blockTag);
        if (blocks.Length == 0) return;

        // 전체 Bounds 계산
        Bounds totalBounds = blocks[0].GetComponent<Renderer>().bounds;
        for (int i = 1; i < blocks.Length; i++)
        {
            totalBounds.Encapsulate(blocks[i].GetComponent<Renderer>().bounds);
        }

        // 외곽선 큐브 생성
        GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
        outline.name = "TotalOutline";
        outline.transform.position = totalBounds.center;
        outline.transform.localScale = totalBounds.size * outlineScale;

        // 머티리얼 설정
        Material mat = new Material(Shader.Find("Custom/OutlineShader")); // 앞서 만든 쉐이더
        Color baseColor = blocks[0].GetComponent<Renderer>().material.color;
        mat.color = baseColor * darknessFactor;

        var renderer = outline.GetComponent<Renderer>();
        renderer.material = mat;
    }
}

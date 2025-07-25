using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOutlineFromParentColor : MonoBehaviour
{
    public float outlineScale = 1.05f;
    public float darknessFactor = 0.5f; // 0 ~ 1 (값이 작을수록 더 어두움)

    void Start()
    {
        var parentRenderer = GetComponent<Renderer>();
        if (parentRenderer == null) return;

        Color baseColor = parentRenderer.material.color;
        Color outlineColor = baseColor * darknessFactor;

        // 외곽선 큐브 생성
        GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
        outline.name = "OutlineCube";
        outline.transform.SetParent(transform);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.localRotation = Quaternion.identity;
        outline.transform.localScale = Vector3.one * outlineScale;

        // 머티리얼 생성 및 컬러 설정
        Material mat = new Material(Shader.Find("Custom/OutlineShader")); // 앞서 만든 쉐이더 사용
        mat.color = outlineColor;

        var rend = outline.GetComponent<Renderer>();
        rend.material = mat;
    }
}

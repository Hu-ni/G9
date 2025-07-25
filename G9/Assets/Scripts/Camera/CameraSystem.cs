using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    // 카메라 시스템 관리
    // 자유 시점 변환과 해제
    // 만약 배경이 아닌 벽이 가까울 때 투명처리 (여기서 하는게 맞나?)

    private enum CameraMode
    {
        Follow,
        FreeLook,
        ZoomIn,
        ZoomOut
    }

    [SerializeField]
    private CameraFollow _follow;   // Obj 따라가기
    [SerializeField]
    private CameraTracker _tracker; // Obj 바라보기

    public CameraFollow Follow { get => _follow; private set => _follow = value; }
    public CameraTracker Tracker { get => _tracker; private set => _tracker = value; }

    
    void Update()
    {

    }
}

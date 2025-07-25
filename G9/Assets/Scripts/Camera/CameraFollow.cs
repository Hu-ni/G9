using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 카메라 따라가기 Script
    // Obj의 위치를 받아와 카메라 위치와 동기화.
    // 자유 시점일 때 동기화 해제
    [SerializeField]
    private GameObject Obj;

    private Vector3 _offset;
    private Vector3 _centerOffset;

    public bool isCenter = true;    // Obj를 카메라 중앙에

    void Start()
    {
        float offsetX = transform.position.x - Obj.transform.position.x;
        float offsetY = transform.position.y - Obj.transform.position.y;
        //_offset = new Vector3(offsetX, offsetY, transform.position.z);    // 현재 flappy bird에서만 사용하기 때문에 임시 주석처리
        _offset = new Vector3(offsetX, transform.position.y, transform.position.z);

        _centerOffset = Vector3.zero;
        _centerOffset.z = transform.position.z;
    }

    // 플레이어 이동 이후 처리하도록 시점 변경
    void LateUpdate()
    {
        if (isCenter)    // 카메라 중앙에 Obj 위치
            transform.position = Obj.transform.position + _centerOffset;
        else            // 현재 카메라 위치에서 Obj 따라가기
            transform.position = new Vector3(Obj.transform.position.x + _offset.x, transform.position.y, Obj.transform.position.z + _offset.z);
    }
}

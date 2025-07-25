using G9.Game.Interactive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : Character
{

    // NonPlayerCharacter 스크립트
    // 플레이어의 상호작용 시 관련 행동하도록 하기
    // 패턴에 따라 이동
    [SerializeField]
    private Transform[] _wayPoints;

    [SerializeField]
    private UnityEvent fallbackEvent;   // Inspector 에서만 수정할 수 있게
    private IInteractable[] _interactions;  // NPC 상호작용 다중 역할 부여

    private int currentIndex = 0;
    public bool isInteractive = false;

    private void Start()
    {
        _interactions = GetComponents<IInteractable>();
    }

    // NPC 이벤트 발생
    public void Invoke()
    {
        fallbackEvent?.Invoke();
    }

    // 상호작용 중 하나 실행
    void TryInteract<T>() where T : IInteractable
    {
        var comp = GetComponent<T>();
        comp?.Interact();
    }

    protected override Vector3 GetMoveDirection()
    {
        if (isInteractive || _wayPoints.Length == 0) return Vector3.zero;

        Vector3 target = _wayPoints[currentIndex].position;
        Vector3 dir = target - transform.position;
        if (dir.sqrMagnitude < 0.1f)
        {
            currentIndex = (currentIndex + 1) % _wayPoints.Length;
        }

        return dir;
    }
}

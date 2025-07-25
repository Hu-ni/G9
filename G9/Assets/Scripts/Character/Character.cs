using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // 공통 캐릭터 클레스
    // 플레이어, NPC 등 모두 이 클레스를 참조.
    // 공통 데이터: 이름, 골드, 속도

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _speed;

    public string Name { get => _name; protected set => _name = value; }
    public int Gold { get => _gold; protected set => _gold = value; }
    public int Speed { get => _speed; protected set => _speed = value; }

    public virtual void MoveTo(Vector3 dir)
    {
        // TODO: rigidbody로 움직이게 변경
        //_rigidbody.velocity = dir.normalized * Speed * Time.deltaTime;
        transform.Translate(dir.normalized * Speed * Time.deltaTime);
    }

    protected abstract Vector3 GetMoveDirection();
    protected virtual void Update()
    {
        Vector3 dir = GetMoveDirection();
        if(dir != Vector3.zero)
            MoveTo(dir);
    }

}

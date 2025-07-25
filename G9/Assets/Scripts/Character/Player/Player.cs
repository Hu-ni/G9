using G9.Game.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player : Character
{
    // 플레이어 클래스
    // 상호작용, 움직임 정의
    private static Player player;
    public static Player Instance { get => player; }

    private void Start()
    {
        if(Instance != null)
            Destroy(this);
        player = this;

        if (GameStateManager.HasSavedState)
        {
            transform.position = GameStateManager.PlayerPosition;
        }
    }

    protected override void Update()
    {
        base.Update();

        // 점프는 추 후에 개발
        //if(Input.GetAxisRaw("Jump") != 0)
        //{
        //    Jump();
        //}
    }

    // 움직임 정의
    protected override Vector3 GetMoveDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector3(x, y, 0);
    }
}

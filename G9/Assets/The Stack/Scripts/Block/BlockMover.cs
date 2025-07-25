using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace G9.MiniGame.TheStack
{
    public class BlockMover
    {
        private const float BoundSize = 3.5f;
        private const float MovingBoundsSize = 3f;  // Move에서만 사용
        private const float StackMovingSpeed = 5.0f;    //Move에서만 사용
        private const float BlockMovingSpeed = 3.5f;    //Move에서만 사용

        private float blockTransition = 0f; // Move로 이동

        public void Reset() => blockTransition = 0f;


        // 
        // 블럭 움직임 왔다 갔다 핑퐁 사용.
        public void MoveBlock(Transform block, int stackCount, bool isMovingX, float secondaryPosition)
        {
            blockTransition += Time.deltaTime * BlockMovingSpeed;
            float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

            if (isMovingX)
            {
                block.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
            }
            else
            {
                block.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
            }
        }
    }
}
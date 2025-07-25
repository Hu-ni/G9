using G9.MiniGame.TheStack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer
{
    private const float ErrorMargin = 0.1f;

    public event Action OnComboSuccess;

    private RubbleSpawner _spawner;
    private ComboTracker _comboTracker;

    public BlockPlacer(RubbleSpawner spawner, ComboTracker comboTracker)
    {
        _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
        _comboTracker = comboTracker ?? throw new ArgumentNullException(nameof(comboTracker));
    }

    public bool PlaceBlock(Transform block, bool isMovingX, Vector3 prevBlockPosition, 
            ref Vector3 stackBounds, ref float secondaryPosition)
    {
        Vector3 lastPosition = block.transform.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;    //깔려있는 블럭 위치 - 현재 블럭 위치
            bool isNegativeNum = (deltaX < 0) ? true : false;

            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)
            {
                stackBounds.x -= deltaX;    //차이만큼 크기 조절
                if (stackBounds.x <= 0) // 차이가 크기보다 더 클 때 리턴
                {
                    return false;
                }

                float middle = (prevBlockPosition.x + lastPosition.x) / 2;  // 중앙 왜?
                block.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = block.localPosition;
                tempPosition.x = middle;
                block.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaX / 2f;
                _spawner.CreateRubble(block.gameObject, 
                    new Vector3(isNegativeNum
                            ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                            : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                        , lastPosition.y
                        , lastPosition.z),
                    new Vector3(deltaX, 1, stackBounds.y), block.parent
                );
                _comboTracker.ResetCombo();
            }
            else
            {
                _comboTracker.IncrementCombo(ref stackBounds);
                block.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        else
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                block.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = block.localPosition;
                tempPosition.z = middle;
                block.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaZ / 2f;
                _spawner.CreateRubble(block.gameObject,
                    new Vector3(
                        lastPosition.x
                        , lastPosition.y
                        , isNegativeNum
                            ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                            : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ),
                    block.parent
                );
                _comboTracker.ResetCombo();
            }
            else
            {
                _comboTracker.IncrementCombo(ref stackBounds);
                block.localPosition = prevBlockPosition + Vector3.up;
            }
        }

        secondaryPosition = (isMovingX) ? block.localPosition.x : block.localPosition.z;

        return true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace G9.MiniGame.TheStack
{
    public class ComboTracker
    {
        private int comboCount = 0;
        private int maxCombo = 0;
        private const float BoundSize = 3.5f;

        public int Combo => comboCount;
        public int MaxCombo => maxCombo;

        public void Initialize()
        {
            comboCount = 0;
            maxCombo = 0;
        }

        public void ResetCombo() => comboCount = 0;
        public void IncrementCombo(ref Vector3 stackBounds)
        {
            comboCount++;
            if (comboCount > maxCombo)
                maxCombo = comboCount;

            if (comboCount % 5 == 0)
            {
                Debug.Log("5Combo Success!");
                stackBounds += new Vector3(0.5f, 0.5f);
                stackBounds.x = Mathf.Min(stackBounds.x, BoundSize);
                stackBounds.y = Mathf.Min(stackBounds.y, BoundSize);
            }
        }
    }
}

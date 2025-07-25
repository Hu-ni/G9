using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace G9.Game.DTO
{
    public static class GameStateManager
    {
        public static Vector3 PlayerPosition = Vector3.zero;
        public static int SlideIndex = 0;
        public static bool HasSavedState = false;
    }
}

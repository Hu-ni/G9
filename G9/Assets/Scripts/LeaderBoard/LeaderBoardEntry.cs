using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G9.Game.LeaderBoard
{
    [Serializable]
    public class LeaderBoardEntry
    {
        public string playerName;
        public int score;
        public float playTime;
        public string timestamp; // 기록 시간 (옵션)

        // 게임별 특수 데이터 (ex. "maxCombo": "25", "perfectClear": "true")
        public Dictionary<string, string> extraData = new();
    }
}

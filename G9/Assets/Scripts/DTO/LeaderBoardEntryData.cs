using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTO
{
    [System.Serializable]
    public class LeaderBoardEntryData
    {
        public string playername;
        public int score;
        public int combo;
    }


    [System.Serializable]
    public class LeaderboardDataWrapper
    {
        public List<LeaderBoardEntryData> entries = new List<LeaderBoardEntryData>();
    }
}

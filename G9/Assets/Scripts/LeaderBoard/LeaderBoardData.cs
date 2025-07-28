using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G9.Game.LeaderBoard
{
    [Serializable]
    public class LeaderBoardData
    {
        public string gameId;
        public string displayName;
        public List<LeaderBoardEntry> entries = new();
    }
}

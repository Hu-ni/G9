using G9.Const;
using G9.Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G9.Game.LeaderBoard
{
    public class GameLeaderBoardSystem
    {
        public Dictionary<string, LeaderBoardData> leaderBoards = new();

        public void Initialize()
        {
            leaderBoards[ConstValues.FlappyPlane] = LeaderboardFileUtil.LoadLeaderboard(ConstValues.FlappyPlane);
            leaderBoards[ConstValues.TheStack] = LeaderboardFileUtil.LoadLeaderboard(ConstValues.TheStack);
        }

        // 새로운 기록 추가
        public void AddEntry(string gameId, LeaderBoardEntry entry)
        {
            if (!leaderBoards.ContainsKey(gameId))
                leaderBoards[gameId] = new LeaderBoardData { gameId = gameId, displayName = gameId };

            leaderBoards[gameId].entries.Add(entry);
            // 예: score 기준 내림차순 정렬
            leaderBoards[gameId].entries = leaderBoards[gameId].entries
                .OrderByDescending(e => e.score)
                .Take(100)
                .ToList();
        }

        public List<LeaderBoardEntry> GetTopEntries(string gameId, int count)
        {
            if (!leaderBoards.ContainsKey(gameId)) return new();
            return leaderBoards[gameId].entries.Take(count).ToList();
        }
    }
}

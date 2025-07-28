using G9.Game.LeaderBoard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace G9.Game.Util
{
    public static class LeaderboardFileUtil
    {
 
        // 파일 저장 (gameId별로 분리)
        public static void SaveLeaderBoard(string gameId, LeaderBoardData leaderBoard)
        {
            string json = JsonUtility.ToJson(leaderBoard, true); // true: 예쁘게 포맷
            string path = GetFilePath(gameId);
            File.WriteAllText(path, json);
        }


        // 파일 불러오기 (없으면 빈 Leaderboard 반환)
        public static LeaderBoardData LoadLeaderboard(string gameId)
        {
            string path = GetFilePath(gameId);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<LeaderBoardData>(json);
            }
            return new LeaderBoardData { gameId = gameId, displayName = gameId, entries = new List<LeaderBoardEntry>() };
        }


        private static string GetFilePath(string gameId)
        {
            return Path.Combine(Application.persistentDataPath, $"leaderboard_{gameId}.json");
        }
    }
}

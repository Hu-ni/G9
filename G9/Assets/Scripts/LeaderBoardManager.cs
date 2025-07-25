using Assets.Scripts.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace G9.Game
{
    public class LeaderBoardManager
    {
        public List<LeaderBoardEntryData> entries = new List<LeaderBoardEntryData>();

        public void AddEntry(LeaderBoardEntryData entry)
        {
            entries.Add(entry);
            SortEntries(); // 점수 기준 정렬 등
        }

        void SortEntries()
        {
            entries.Sort((a, b) => b.score.CompareTo(a.score)); // 높은 점수 순
        }

        public List<LeaderBoardEntryData> GetTopEntries(int count)
        {
            return entries.Take(count).ToList();
        }

        public void SaveLeaderboard()
        {
            string json = JsonUtility.ToJson(new LeaderboardDataWrapper { entries = this.entries });
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "leaderboard.json"), json);
        }

        public void LoadLeaderboard()
        {
            string path = Path.Combine(Application.persistentDataPath, "leaderboard.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                entries = JsonUtility.FromJson<LeaderboardDataWrapper>(json).entries;
            }
        }
    }
}

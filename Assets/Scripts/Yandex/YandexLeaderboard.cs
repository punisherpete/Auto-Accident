using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class YandexLeaderboard : MonoBehaviour
{
    private LeaderboardView _leaderboardView;

    private const string _leaderboardName = "TopRacers";

    private void OnLevelWasLoaded(int level)
    {
        _leaderboardView = FindObjectOfType<LeaderboardViewHandler>().LeaderboardView;
    }

    public void GetTop5ResultsFromLeaderboard()
    {
        List<PlayerInfoLeaderboard> top5Players = new List<PlayerInfoLeaderboard>();
        print("g");
#if UNITY_EDITOR
        for (int i = 0; i < 5; i++)
        {
            top5Players.Add(new PlayerInfoLeaderboard("name", i));
        }

        _leaderboardView.ConstructLeaderboard(top5Players);

        return;
#endif

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");

            int resultsAmount = result.entries.Length;

            resultsAmount = Mathf.Clamp(resultsAmount, 1, 5);

            for (int i = 0; i < resultsAmount; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymos";

                int score = result.entries[i].score;

                top5Players.Add(new PlayerInfoLeaderboard(name, score));
            }

            _leaderboardView.ConstructLeaderboard(top5Players);
        });
    }

    public void AddPlayerToLeaderboard(int score)
    {
        Leaderboard.SetScore(_leaderboardName, score);
    }
}


public class PlayerInfoLeaderboard
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerInfoLeaderboard(string name, int score)
    {
        Name = name;
        Score = score;
    }
}

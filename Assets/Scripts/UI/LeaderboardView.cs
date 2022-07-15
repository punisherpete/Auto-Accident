using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private GameObject _leaderboardElementPrefab;

    private YandexLeaderboard _yandexLeaderboard;

    private List<GameObject> _spawnedElements = new List<GameObject>();

    public void SetYandexLeaderboard(YandexLeaderboard yandexLeaderboard)
    {
        _yandexLeaderboard = yandexLeaderboard;
    }

    public void LeaderboardButton()
    {
        _yandexLeaderboard.GetTop5ResultsFromLeaderboard();
    }

    public void ConstructLeaderboard(List<PlayerInfoLeaderboard> playersInfo)
    {
        ClearLeaderboard();

        foreach (PlayerInfoLeaderboard info in playersInfo)
        {
            GameObject leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _parentObject);

            LeaderboardElement leaderboardElement = leaderboardElementInstance.GetComponent<LeaderboardElement>();
            leaderboardElement.Construct(info.Name, info.Score);

            _spawnedElements.Add(leaderboardElementInstance);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
            Destroy(element);

        _spawnedElements = new List<GameObject>();
    }
}

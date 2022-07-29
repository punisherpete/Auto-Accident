using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardViewHandler : MonoBehaviour
{
    [field: SerializeField] public LeaderboardView LeaderboardView { get; private set; }

    private void Start()
    {
        LeaderboardView.SetYandexLeaderboard(FindObjectOfType<YandexLeaderboard>());
    }
}

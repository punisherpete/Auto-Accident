using UnityEngine;
using GameAnalyticsSDK;
using System.Collections.Generic;

public class AnalyticsInitialize: MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameAnalytics.Initialize();
        GameInitialize();
    }

    private void GameInitialize()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game_start", new Dictionary<string, object>()
        {
            {"count", PlayerPrefs.GetInt("GameCount", 0) }
        });

        PlayerPrefs.SetInt("GameCount", PlayerPrefs.GetInt("GameCount") + 1);
    }
}

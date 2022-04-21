using UnityEngine;
using GameAnalyticsSDK;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameAnalyticsObject", menuName = "GameAssets/GameAnalyticsObject")]
public class GameAnalyticsObject : ScriptableObject
{
    public void OnGameInitialize(int sessionCount)
    {
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game_start", new Dictionary<string, object>()
        {
            {"count",sessionCount }
        });
    }

    public void OnLevelStart(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start" , new Dictionary<string, object>()
        {
            {"level", levelNumber }
        });
    }

    public void OnLevelComplete(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete", new Dictionary<string, object>()
        {
            {"level", levelNumber },
            {"time_spent",(int)Time.timeSinceLevelLoad }
        });
    }

    public void OnFail(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "fail", new Dictionary<string, object>()
        {
            {"level", levelNumber },
            {"time_spent",(int)Time.timeSinceLevelLoad }
        });
    }

    public void OnLevelRestart(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "restart", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }
}

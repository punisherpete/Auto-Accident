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

    public void OnSoftSpend(string type, string name, int amount, int count)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "soft_spent", new Dictionary<string, object>()
        {
            {"type",type },
            {"name",name },
            {"amount",amount },
            {"count",count }
        });
    }

    public void OnGameExit(string registrationDate, int sessionCount, int daysInGame)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "reg_day", new Dictionary<string, object>()
        {
            {"date", registrationDate }
        });
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "session_count", new Dictionary<string, object>()
        {
            {"count", sessionCount }
        });
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "days_in_game", new Dictionary<string, object>()
        {
            {"day", daysInGame }
        });
    }
    
    public void OnGameExit(string registrationDate, int sessionCount, int daysInGame, int currentSoft)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "reg_day", new Dictionary<string, object>()
        {
            {"date", registrationDate }
        });
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "session_count", new Dictionary<string, object>()
        {
            {"count", sessionCount }
        });
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "days_in_game", new Dictionary<string, object>()
        {
            {"day", daysInGame }
        });
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "currentSoft", new Dictionary<string, object>()
        {
            {"current_soft", currentSoft }
        });
    }
}

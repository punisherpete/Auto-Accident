using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppMetricaObject", menuName = "GameAssets/AppMetricaObject")]
public class AppMetricaObject : ScriptableObject
{
    public void OnGameInitialize(int sessionCount)
    {
        AppMetrica.Instance.ReportEvent("game_start", new Dictionary<string, object>()
        {
            {"count",sessionCount }
        });
    }

    public void OnLevelStart(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("level_start", new Dictionary<string, object>()
        {
            {"level", levelNumber }
        });
    }

    public void OnLevelComplete(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("level_complete", new Dictionary<string, object>()
        {
            {"level", levelNumber },
            {"time_spent",(int)Time.timeSinceLevelLoad }
        });
    }

    public void OnFail(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("fail", new Dictionary<string, object>()
        {
            {"level", levelNumber },
            {"time_spent",(int)Time.timeSinceLevelLoad }
        });
    }

    public void OnLevelRestart(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("restart", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }

    public void OnSoftSpend(string type,string name,int amount,int count)
    {
        AppMetrica.Instance.ReportEvent("soft_spent", new Dictionary<string, object>()
        {
            {"type", type },
            {"name", name },
            {"amount", amount },
            {"count", count }
        });
    }

    public void OnGameExit(string registrationDate, int sessionCount, int daysInGame)
    {
        AppMetrica.Instance.ReportEvent("reg_day", new Dictionary<string, object>()
        {
            {"date", registrationDate }
        });
        AppMetrica.Instance.ReportEvent("session_count", new Dictionary<string, object>()
        {
            {"count", sessionCount }
        });
        AppMetrica.Instance.ReportEvent("days_in_game", new Dictionary<string, object>()
        {
            {"day", daysInGame }
        });
    }

    public void OnGameExit(string registrationDate, int sessionCount, int daysInGame, int currentSoft)
    {
        AppMetrica.Instance.ReportEvent("reg_day", new Dictionary<string, object>()
        {
            {"date", registrationDate }
        });
        AppMetrica.Instance.ReportEvent("session_count", new Dictionary<string, object>()
        {
            {"count", sessionCount }
        });
        AppMetrica.Instance.ReportEvent("days_in_game", new Dictionary<string, object>()
        {
            {"day", daysInGame }
        });
        AppMetrica.Instance.ReportEvent("current_soft", new Dictionary<string, object>()
        {
            {"current_soft", currentSoft }
        });
    }


}

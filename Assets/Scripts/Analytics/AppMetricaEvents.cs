using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppMetricaObject", menuName = "GameAssets/AppMetricaObject")]
public class AppMetricaEvents : ScriptableObject
{

    /*public void OnApplicationQuit()
    {
        AppMetrica.Instance.ReportEvent("reg_day", new Dictionary<string, object>()
        {
            {"date",_data.GetRegistrationDate() }
        });
        AppMetrica.Instance.ReportEvent("session_count", new Dictionary<string, object>()
        {
            {"count",_data.GetSessionCount() }
        });
        AppMetrica.Instance.ReportEvent("days_in_game", new Dictionary<string, object>()
        {
            {"day",_data.GetNumberDaysAfterRegistration() }
        });
    }*/

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
}

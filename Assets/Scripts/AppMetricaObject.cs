using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "AppMetricaObject", menuName = "GameAssets/AppMetricaObject")]
public class AppMetricaObject : ScriptableObject
{
    private DateTime _levelStartTime;
    private bool _isFirstStart;

    /*public void Init()
    {
        _isFirstStart = !PlayerPrefs.HasKey(Game.IS_FIRST_START);
        GameStart();
    }
    
    public void GameStart()
    {
        if (_isFirstStart)
        {
            PlayerPrefs.SetString(Game.REG_DAY, DateTime.Now.ToString());
            PlayerPrefs.SetInt(Game.IS_FIRST_START, 1);
        }

        int session_count = PlayerPrefs.GetInt(Game.SESSION_COUNT, 0);
        session_count++;

        PlayerPrefs.SetInt(Game.SESSION_COUNT, session_count);

        Dictionary<string, object> eventProps2 = new Dictionary<string, object>();
        eventProps2.Add("count", session_count);

        AppMetrica.Instance.ReportEvent("game_start", eventProps2);
        
        *//*DateTime reg_day = DateTime.Parse(PlayerPrefs.GetString(Game.REG_DAY));*//*

        int days = (DateTime.Now - reg_day).Days;
    }
    public void LevelStart(int levelNumber)
    {
        _levelStartTime = DateTime.Now;

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("level", levelNumber);
        AppMetrica.Instance.ReportEvent("level_start", eventProps);
    }

    public void LevelComplete(int levelNumber, int money)
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("time_spent", Convert.ToInt32((DateTime.Now - _levelStartTime).TotalSeconds));
        eventProps.Add("level", levelNumber);

        AppMetrica.Instance.ReportEvent("level_complete", eventProps);
    }

    public void LevelFail(int levelNumber)
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("time_spent", Convert.ToInt32((DateTime.Now - _levelStartTime).TotalSeconds));
        eventProps.Add("level", levelNumber);

        AppMetrica.Instance.ReportEvent("fail", eventProps);
    }

    public void SpendMoney(string type, string name, int amount)
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("type", type);
        eventProps.Add("name", name);
        eventProps.Add("amount", amount.ToString());

        AppMetrica.Instance.ReportEvent("soft_spent", eventProps);
    }

    public void MainMenu()
    {
        AppMetrica.Instance.ReportEvent("main_menu");
    }*/
}

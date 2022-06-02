using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticManager : MonoBehaviour
{
    [SerializeField] private AppMetricaObject _appMetricaObject;
    [SerializeField] private GameAnalyticsObject _gameAnalyticsObject;

    public void SendEventOnGameInitialize(int sessionCount)
    {
        _appMetricaObject.OnGameInitialize(sessionCount);
        _gameAnalyticsObject.OnGameInitialize(sessionCount);
    }

    public void SendEventOnLevelStart(int levelNumber)
    {
        _appMetricaObject.OnLevelStart(levelNumber);
        _gameAnalyticsObject.OnLevelStart(levelNumber); 
    }

    public void SendEventOnLevelComplete(int levelNumber)
    {
        _appMetricaObject?.OnLevelComplete(levelNumber);
        _gameAnalyticsObject?.OnLevelComplete(levelNumber);
    }

    public void SendEventOnFail(int levelNumber)
    {
        _appMetricaObject.OnFail(levelNumber);
        _gameAnalyticsObject?.OnFail(levelNumber);
    }

    public void SendEventOnLevelRestart(int levelNumber)
    {
        _appMetricaObject?.OnLevelRestart(levelNumber);
        _gameAnalyticsObject?.OnLevelRestart(levelNumber);
    }

    public void SendEventOnSoftSpend(string purchaseType, string storeName, int purchaseAmount, int purchasesCount)
    {
        _appMetricaObject.OnSoftSpend(purchaseType, storeName, purchaseAmount, purchasesCount);
        _gameAnalyticsObject.OnSoftSpend(purchaseType,storeName, purchaseAmount, purchasesCount);
    }

    public void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame)
    {
        _appMetricaObject.OnGameExit(registrationDate, sessionCount, daysInGame);
        _gameAnalyticsObject.OnGameExit(registrationDate,sessionCount, daysInGame);
    }

    public void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame, int currentSoft)
    {
        _appMetricaObject.OnGameExit(registrationDate, sessionCount, daysInGame,currentSoft);
        _gameAnalyticsObject.OnGameExit(registrationDate, sessionCount, daysInGame,currentSoft);
    }
}

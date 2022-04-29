using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneManager : Data
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private AppMetricaEvents _appMetricaObject;
    [SerializeField] private GameAnalyticsObject _gameAnalyticsObject;

    private void Awake()
    {
        Load();
        SetLevelIndex(SceneManager.GetActiveScene().buildIndex);
        Save();
        _levelText.text = $"Level {GetDisplayedLevelNumber()}";
    }

    private void Start()
    {
        _appMetricaObject.OnLevelStart(GetLevelIndex());
        _gameAnalyticsObject.OnLevelStart(GetLevelIndex());
    }

    private void OnApplicationQuit()
    {
        _appMetricaObject.OnGameExit(GetRegistrationDate(), GetSessionCount(), GetNumberDaysAfterRegistration());
        _gameAnalyticsObject.OnGameExit(GetRegistrationDate(), GetSessionCount(), GetNumberDaysAfterRegistration());
        Save();
    }

    public void LevelFail()
    {
        _appMetricaObject.OnFail(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnFail(GetDisplayedLevelNumber());
    }

    public void LoadScene(int index)
    {
        _appMetricaObject.OnLevelComplete(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnLevelComplete(GetDisplayedLevelNumber());
        AddDisplayedLevelNumber();
        Save();
        SceneManager.LoadScene(index);
    }

    public void ReloadScene()
    {
        _appMetricaObject.OnLevelRestart(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnLevelRestart(GetDisplayedLevelNumber());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

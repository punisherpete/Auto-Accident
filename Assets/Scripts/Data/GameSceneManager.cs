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
        _levelText.text = $"Level {SceneManager.GetActiveScene().buildIndex}";
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
        _appMetricaObject.OnFail(GetLevelIndex());
        _gameAnalyticsObject.OnFail(GetLevelIndex());
    }

    public void LoadScene(int index)
    {
        _appMetricaObject.OnLevelComplete(GetLevelIndex());
        _gameAnalyticsObject.OnLevelComplete(GetLevelIndex());
        Save();
        SceneManager.LoadScene(index);
    }

    public void ReloadScene()
    {
        _appMetricaObject.OnLevelRestart(GetLevelIndex());
        _gameAnalyticsObject.OnLevelRestart(GetLevelIndex());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

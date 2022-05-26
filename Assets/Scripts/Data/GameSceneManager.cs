using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneManager : Data
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private AppMetricaEvents _appMetricaObject;
    [SerializeField] private GameAnalyticsObject _gameAnalyticsObject;
    [SerializeField] private bool _isSwitchingLevelsManually = false;
    [SerializeField] private int _nextLevelIndex = 0;

    private void Awake()
    {
        Load();
        SetLevelIndex(SceneManager.GetActiveScene().buildIndex);
        Save();
        _levelText.text = $"Level {GetDisplayedLevelNumber()}";
    }

    private void Start()
    {
        _appMetricaObject.OnLevelStart(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnLevelStart(GetDisplayedLevelNumber());
    }

    private void OnApplicationQuit()
    {
        _appMetricaObject.OnGameExit(GetRegistrationDate(), GetSessionCount(), GetNumberDaysAfterRegistration());
        _gameAnalyticsObject.OnGameExit(GetRegistrationDate(), GetSessionCount(), GetNumberDaysAfterRegistration());
        Save();
    }

    private void SaveNextLevelIndex()
    {
        SetLevelIndex(_nextLevelIndex);
        AddDisplayedLevelNumber();
    }

    public void LevelFail()
    {
        _appMetricaObject.OnFail(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnFail(GetDisplayedLevelNumber());
    }

    public void LoadNextScene()
    {
        _appMetricaObject.OnLevelComplete(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnLevelComplete(GetDisplayedLevelNumber());
        SaveNextLevelIndex();
        Save();
        if(_isSwitchingLevelsManually)
            SceneManager.LoadScene(_nextLevelIndex);
        else
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ReloadScene()
    {
        _appMetricaObject.OnLevelRestart(GetDisplayedLevelNumber());
        _gameAnalyticsObject.OnLevelRestart(GetDisplayedLevelNumber());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private AnalyticManager _analytic;
    [SerializeField] private Data _data;

    private int _nextLevelIndex;

    private void Awake()
    {
        _data.Load();
        _data.SetLevelIndex(SceneManager.GetActiveScene().buildIndex);
        _data.Save();
        _levelText.text = $"Level {_data.GetDisplayedLevelNumber()}";
    }

    private void Start()
    {
        _analytic.SendEventOnLevelStart(_data.GetDisplayedLevelNumber());
    }

    private void OnApplicationQuit()
    {
        _analytic.SendEventOnGameExit(_data.GetRegistrationDate(), _data.GetSessionCount(), _data.GetNumberDaysAfterRegistration(), _data.GetCurrentSoft());
        _data.Save();
    }

    public void LevelFail()
    {
        _analytic.SendEventOnFail(_data.GetDisplayedLevelNumber());
    }

    public void LoadNextScene()
    {
        _analytic.SendEventOnLevelComplete(_data.GetDisplayedLevelNumber());
        _data.Save();
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            _nextLevelIndex = 1;
        else
            _nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SaveNextLevelIndex();
        SceneManager.LoadScene(_nextLevelIndex);
    }

    public void ReloadScene()
    {
        _analytic.SendEventOnLevelRestart(_data.GetDisplayedLevelNumber());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SaveNextLevelIndex()
    {
        _data.SetLevelIndex(_nextLevelIndex);
        _data.AddDisplayedLevelNumber();
    }
}

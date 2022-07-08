using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Data _data;

    private int _nextLevelIndex;

    private void Awake()
    {
        _data.Load();
        _data.SetLevelIndex(SceneManager.GetActiveScene().buildIndex);
        PointsTransmitter.Instance.SetPoints(_data.GetCurrentSoft());
        _data.Save();
        _levelText.text = $"Level {_data.GetDisplayedLevelNumber()}";
    }

    private void OnApplicationQuit()
    {
        _data.Save();
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            _nextLevelIndex = 1;
        else
            _nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        _data.SetLevelIndex(_nextLevelIndex);
        _data.AddDisplayedLevelNumber();
        _data.Save();
        SceneManager.LoadScene(_nextLevelIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

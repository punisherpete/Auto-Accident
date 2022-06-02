using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreensaverSceneManager : MonoBehaviour
{
    [SerializeField] private bool _isRemoveDataOnStart = false;
    [SerializeField] private AnalyticManager _analytic;
    [SerializeField] private Data _data;

    private void Awake()
    {
        if (_isRemoveDataOnStart)
            _data.RemoveData();
        CheckSaveFile();
        _data.AddSession();
        _data.SetLastLoginDate(DateTime.Now);
        _analytic.SendEventOnGameInitialize(_data.GetSessionCount());
        _data.Save();
        SceneManager.LoadScene(_data.GetLevelIndex());
    }

    private void CheckSaveFile()
    {
        if (PlayerPrefs.HasKey(_data.GetKeyName()))
        {
            Debug.Log("Load old data file");
            _data.Load();
        }
        else
        {
            Debug.Log("Create new save data file");
            _data.Save();
            _data.SetLevelIndex(1);
            _data.SetDateRegistration(DateTime.Now);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreensaverSaveChecker : Data
{
    [SerializeField] private bool _isRemoveDataOnStart = false;
    [SerializeField] private AppMetricaEvents _appMetricaEvents;

    private void Awake()
    {
        if (_isRemoveDataOnStart)
            RemoveData();
        CheckSaveFile();
        AddSession();
        SetLastLoginDate(DateTime.Now);
        _appMetricaEvents.OnGameInitialize();
        Save();
        SceneManager.LoadScene(_options.LevelNumber);
    }



    private void CheckSaveFile()
    {
        if (PlayerPrefs.HasKey(_dataKeyName))
        {
            Debug.Log("Load old file");
            Load();
        }
        else
        {
            Debug.Log("Create new save file");
            Save();
            SetLevelIndex(1);
            SetDateRegistration(DateTime.Now);
        }
    }
}

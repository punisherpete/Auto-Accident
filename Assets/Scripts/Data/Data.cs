using System;
using UnityEngine;

public class Data : MonoBehaviour
{
    private const string _dataKeyName = "AutoAccident";
    private SaveOptions _options = new SaveOptions();

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_options);
        PlayerPrefs.SetString(_dataKeyName, json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_dataKeyName) == false)
        {
            _options = new SaveOptions();
            Save();
        }
        else
            _options = JsonUtility.FromJson<SaveOptions>(PlayerPrefs.GetString(_dataKeyName));
    }

    public int GetLevelIndex()
    {
        return _options.LevelNumber;
    }

    public void SetLevelIndex(int index)
    {
        _options.LevelNumber = index;
    }
}

[Serializable]
public class SaveOptions
{
    public int LevelNumber = 0;
}


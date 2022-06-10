using System;
using UnityEngine;

public class Data : MonoBehaviour
{
    protected const string _dataKeyName = "AutoAccident";
    protected SaveOptions _options = new SaveOptions();

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

    [ContextMenu("RemoveData")]
    public void RemoveData()
    {
        PlayerPrefs.DeleteKey(_dataKeyName);
        _options = new SaveOptions();
    }

    public void SetLevelIndex(int index)
    {
        _options.LevelNumber = index;
    }

    public void SetDateRegistration(DateTime date)
    {
        _options.RegistrationDate = date.ToString();
    }

    public void SetLastLoginDate(DateTime date)
    {
        _options.LastLoginDate = date.ToString();
    }

    public void SetCurrentSoft(int value)
    {
        _options.Soft = value;
    }

    public void AddSession()
    {
        _options.SessionCount++;
    }

    public string GetKeyName()
    {
        return _dataKeyName;
    }

    public int GetLevelIndex()
    {
        return _options.LevelNumber;
    }

    public int GetSessionCount()
    {
        return _options.SessionCount;
    }

    public void AddDisplayedLevelNumber()
    {
        _options.DisplayedLevelNumber++;
    }

    public int GetNumberDaysAfterRegistration()
    {
        return (DateTime.Parse(_options.LastLoginDate) - DateTime.Parse(_options.RegistrationDate)).Days;
    }

    public int GetDisplayedLevelNumber()
    {
        return _options.DisplayedLevelNumber;
    }

    public string GetRegistrationDate()
    {
        return _options.RegistrationDate;
    }

    public int GetCurrentSoft()
    {
        return _options.Soft;
    }
}

[Serializable]
public class SaveOptions
{
    public int LevelNumber;
    public int SessionCount;
    public string LastLoginDate;
    public string RegistrationDate;
    public int DisplayedLevelNumber = 1;
    public int Soft;
}


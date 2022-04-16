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

    public void RemoveData()
    {
        PlayerPrefs.DeleteKey(_dataKeyName);
        _options = new SaveOptions();
    }

    public int GetLevelIndex()
    {
        return _options.LevelNumber;
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

    public void AddSession()
    {
        _options.SessionCount++;
    }

    public int GetNumberDaysAfterRegistration()
    {
        return (DateTime.Parse(_options.LastLoginDate) - DateTime.Parse(_options.RegistrationDate)).Days;
    }
}

[Serializable]
public class SaveOptions
{
    public int LevelNumber;
    public int SessionCount;
    public string LastLoginDate;
    public string RegistrationDate;
}


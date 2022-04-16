using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneManager : Data
{
    [SerializeField] private TMP_Text _levelText;
    /*[SerializeField] private AmplitudeOnGame _amplitude;*/

    private void Awake()
    {
        Load();
        SetLevelIndex(SceneManager.GetActiveScene().buildIndex);
        Save();
        _levelText.text = $"Level {SceneManager.GetActiveScene().buildIndex}";
        /*_amplitude.Inicialize(this);*/
    }

    private void Start()
    {
        /*_amplitude.OnLevelStart();*/
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void LoadScene(int index)
    {
        Save();
        SceneManager.LoadScene(index);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

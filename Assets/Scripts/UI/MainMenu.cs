using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Sprite _mutedImage;
    [SerializeField] private Sprite _unmutedImage;
    [SerializeField] private Image _soundImage;

    [SerializeField] private List<GameObject> _gameElements;

    private void Start()
    {
        LoadSound();
    }

    public void OpenShop()
    {
        _shopPanel.SetActive(true);
        _leaderboardPanel.SetActive(false);
        DisableGameElements();
    }
    
    public void OpenLeaderboard()
    {
        _leaderboardPanel.SetActive(true);
        _shopPanel.SetActive(false);
        DisableGameElements();
    }

    private void DisableGameElements()
    {
        foreach (var element in _gameElements)
            element.SetActive(false);
    }

    public void EnableGameElements()
    {
        foreach (var element in _gameElements)
            element.SetActive(true);
    }

    public void GameStarted()
    {
        gameObject.SetActive(false);
    }

    public void ChangeSound()
    {
        AudioListener.pause = !AudioListener.pause;

        _soundImage.sprite = AudioListener.pause ? _mutedImage : _unmutedImage;
    }

    public void LoadSound()
    {
        _soundImage.sprite = AudioListener.pause ? _mutedImage : _unmutedImage;
    }
}

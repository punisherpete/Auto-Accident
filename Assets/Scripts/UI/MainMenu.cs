using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private LeaderboardView _leaderboardView;

    [SerializeField] private List<GameObject> _gameElements;

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
        _leaderboardView.LeaderboardButton();
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
}

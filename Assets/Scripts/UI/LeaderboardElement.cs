using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private List<Sprite> _playerIcons;
    [SerializeField] private Image _playerIcon;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;

    public void Construct(string name, int score)
    {
        _playerIcon.sprite = _playerIcons[Random.Range(0, _playerIcons.Count - 1)];

        _playerName.text = name;
        _playerScore.text = score.ToString();
    }
}

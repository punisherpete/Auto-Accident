using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardChecker : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _placesTexts;
    [SerializeField] private List<Image> _placeImages;
    [SerializeField] private Color _playerTextColor;
    [SerializeField] private Sprite _playerPlaceSprite;

    public void SetPlayerInPlace(Player player)
    {
        if (_placesTexts.Count == 0 || _placeImages.Count == 0)
            return;
        _placesTexts[0].text = player.Name;
        if (player.Type == PlayerType.Player)
        {
            _placeImages[0].sprite = _playerPlaceSprite;
            _placesTexts[0].color = _playerTextColor;
        }
        _placeImages.RemoveAt(0);
        _placesTexts.RemoveAt(0);
    }
}

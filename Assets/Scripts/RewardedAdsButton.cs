using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour
{
    private Button _button;
    private YandexAds _yandexAds;

    private void Start()
    {
        _button = GetComponent<Button>();
        _yandexAds = FindObjectOfType<YandexAds>();

        _button.onClick.AddListener(_yandexAds.ShowRewardedAd);
    }
}

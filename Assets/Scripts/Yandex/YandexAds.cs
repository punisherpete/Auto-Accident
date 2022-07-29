using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class YandexAds : MonoBehaviour
{
    private CarsObserver _cardObserver;
    private const int _rewardAmount = 250;

    private bool _soundStatus = false;

    private void OnLevelWasLoaded(int level)
    {
        _cardObserver = FindObjectOfType<CarsObserver>();
        _cardObserver.PlayerFinished += ShowFullScreenAd;
    }

    public void ShowRewardedAd()
    {
        VideoAd.Show(OnOpen, OnRewarded, OnRewardedClosed);
    }

    public void ShowFullScreenAd()
    {
        print("Ad Shown!");

#if !UNITY_EDITOR
        InterestialAd.Show(OnOpen, onCloseCallback: OnFullScreenShowed);
#endif
    }

    public void OnOpen()
    {
        _soundStatus = AudioListener.pause;
        AudioListener.pause = true;
    }

    public void OnRewarded()
    {
        PointsTransmitter.Instance.OnTransaction(_rewardAmount);
    }

    public void OnRewardedClosed()
    {
        AudioListener.pause = _soundStatus;
    }

    public void OnFullScreenShowed(bool parameter)
    {
        AudioListener.pause = _soundStatus;
    }
}

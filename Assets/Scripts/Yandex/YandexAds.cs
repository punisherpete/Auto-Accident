using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexAds : MonoBehaviour
{
    private CarsObserver _cardObserver;
    private const int _rewardAmount = 250;
    public Action _adRewarded;

    private bool _soundStatus = false;

    private void OnLevelWasLoaded(int level)
    {
        _cardObserver = FindObjectOfType<CarsObserver>();
        _cardObserver.PlayerFinished += ShowFullScreenAd;
    }

    public void ShowRewardedAd()
    {
#if YANDEX_GAMES
        VideoAd.Shoe(_adOpened, _adReward, _adClosed, _adErrorOccured);
#endif
#if VK_GAMES
        Agava.VKGames.VideoAd.Show(_adRewarded);
#endif
    }

    private void OnEnable()
    {
        _adRewarded += OnRewarded;
    }

    private void OnDisable()
    {
        _adRewarded -= OnRewarded;
    }

    public void ShowFullScreenAd()
    {
        print("Ad Shown!");

#if YANDEX_GAMES
        InterstitialAd.Show(OnOpen,onCloseCallBack:OnFullScreenShowed);
#endif
#if VK_GAMES
        Agava.VKGames.Interstitial.Show();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexAds : MonoBehaviour
{
    private CarsObserver _cardObserver;
    private const int _rewardAmount = 250;
    private bool _soundStatus = false;
    
    
    private Action _adOpened;
    private Action _adRewarded;
    private Action _adClosed;
    private Action<string> _adErrorOccured;
    

    private void OnEnable()
    {
        _adOpened += OnOpen;
        _adRewarded += OnRewarded;
        _adClosed += OnRewardedClosed;
        _adErrorOccured += OnAdErrorOccured;
    }

    private void OnDisable()
    {
        _adOpened -= OnOpen;
        _adRewarded -= OnRewarded;
        _adClosed -= OnRewardedClosed;
        _adErrorOccured -= OnAdErrorOccured;
    }
    
    private void OnLevelWasLoaded(int level)
    {
        _cardObserver = FindObjectOfType<CarsObserver>();
        _cardObserver.PlayerFinished += ShowFullScreenAd;
    }
    
    public void ShowRewardedAd()
    {
#if YANDEX_GAMES
        VideoAd.Show(_adOpened, _adRewarded, _adClosed, _adErrorOccured);
#endif
        //VideoAd.Show(OnOpen, OnRewarded, OnRewardedClosed);

 #if VK_GAMES
         Agava.VKGames.VideoAd.Show(_adRewarded);
 #endif
    }

    public void ShowFullScreenAd()
    {
        print("Ad Shown!");
#if YANDEX_GAMES
        InterestialAd.Show(OnOpen, onCloseCallback: OnFullScreenShowed);
#endif   
        
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
    
    private void OnAdErrorOccured(string obj)
    {
        
    }
}


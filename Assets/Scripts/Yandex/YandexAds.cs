using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class YandexAds : MonoBehaviour
{
    private CarsObserver _cardObserver;

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
        AudioListener.pause = true;
    }

    public void OnRewarded()
    {
        Debug.Log("rewarded");
    }

    public void OnRewardedClosed()
    {
        AudioListener.pause = false;
    }

    public void OnFullScreenShowed(bool parameter)
    {
        AudioListener.pause = false;
    }
}

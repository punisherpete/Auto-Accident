using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyGames;
using System;

public class Ads : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInterstitialAd()
    {
        CrazyAds.Instance.beginAdBreak();
    }

    public void ShowRewardedAd()
    {
        CrazyAds.Instance.beginAdBreakRewarded(RewardedCallback);
    }

    private void RewardedCallback()
    {
        PointsTransmitter.Instance.OnTransaction(250);
    }
}

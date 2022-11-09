using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour
{
    private Button _button;
    private Ads _ads;

    private void Start()
    {
        _button = GetComponent<Button>();

        //here should be crazy ads
        _ads = FindObjectOfType<Ads>();
        _button.onClick.AddListener(_ads.ShowRewardedAd);
    }
}



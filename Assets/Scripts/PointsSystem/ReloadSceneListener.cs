using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSceneListener : MonoBehaviour
{
    private int _pointsInWalletAtLevelStart;

    private void Start()
    {
        _pointsInWalletAtLevelStart = PointsTransmitter.Instance.GetWalletPoints();
    }

    public void DropLevelPoints()
    {
        PointsTransmitter.Instance.DropCollectedPoints(_pointsInWalletAtLevelStart);
    }
}

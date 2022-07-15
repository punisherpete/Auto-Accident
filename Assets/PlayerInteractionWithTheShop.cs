using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCarCustomization))]

public class PlayerInteractionWithTheShop : MonoBehaviour
{
    private PlayerCarCustomization _playerCarCustomization;

    private void Start()
    {
        _playerCarCustomization = GetComponent<PlayerCarCustomization>();
    }

    public void TryBuyProduct(SkinProduct skin)
    {
        if (PointsTransmitter.Instance.GetWalletPoints() >= skin.Price)
            BuyProduct(skin);
    }

    private void BuyProduct(SkinProduct skin)
    {
        PointsTransmitter.Instance.Wallet.OperateWithPoints(-skin.Price);

        _playerCarCustomization.ApplyCustomization(skin.MainCarMat, skin.SecondCarMat);
    }
}

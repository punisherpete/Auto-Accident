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

    public bool TryBuyProduct(SkinProduct skin)
    {
        if (PointsTransmitter.Instance.GetWalletPoints() >= skin.Price)
        {
            BuyProduct(skin);
            return true;
        }

        return false;
    }

    private void BuyProduct(SkinProduct skin)
    {
        PointsTransmitter.Instance.Wallet.OperateWithPoints(-skin.Price);

        if (_playerCarCustomization == null)
            _playerCarCustomization = GetComponent<PlayerCarCustomization>();

        _playerCarCustomization.ApplyCustomization(skin.MainCarMat, skin.SecondCarMat);
    }

    public void UseProduct(SkinProduct skin)
    {
        _playerCarCustomization.ApplyCustomization(skin.MainCarMat, skin.SecondCarMat);
    }
}

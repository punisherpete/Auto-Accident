using UnityEngine;

public class Wallet : IWaletOperation
{
    private int _pointsAmount;

    public void OperateWithPoints(int amount)
    {
        if (amount >= int.MaxValue)
        {
            Debug.LogError("Maximum value of int reached.");
            return;
        }else if(amount < 0 && _pointsAmount < amount)
        {
            Debug.LogError("Minimum limit of points reached.");
            _pointsAmount = 0;
            return;
        }

        _pointsAmount += amount;
    }

    public int GetPointsAmount()
    {
        return _pointsAmount;
    }

    public void Reset(int value)
    {
        _pointsAmount = value;
    }
}

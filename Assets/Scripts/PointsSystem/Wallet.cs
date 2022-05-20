using UnityEngine;

public class Wallet : IWaletOperation
{
    private int _pointsAmount;

    public Wallet(int pointsAmount)
    {
        _pointsAmount = pointsAmount;
    }

    public void AddPoints(int amount)
    {
        if (amount >= int.MaxValue)
        {
            Debug.LogError("Maximum value of int reached.");
            return;
        }

        _pointsAmount += amount;
    }

    public void RemovePoints(int amount)
    {
        if (amount > _pointsAmount)
        {
            Debug.LogError("Points can not be less than 0.");
            return;
        }

        _pointsAmount -= amount;
    }

    public int GetPointsAmount()
    {
        return _pointsAmount;
    }
}

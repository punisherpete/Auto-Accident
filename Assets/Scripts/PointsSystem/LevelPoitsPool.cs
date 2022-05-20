using System;

public class LevelPoitsPool : IPointsPoolTransaction
{
    private int _levelPointsValueCount;
    private GamePoint[] _gamePoints;

    public event Action<int> Withdrawed;

    public LevelPoitsPool(GamePoint[] levelPoints)
    {
        _gamePoints = levelPoints;
        for (int i = 0; i < _gamePoints.Length; i++)
        {
            _levelPointsValueCount += _gamePoints[i].Value;
            _gamePoints[i].BeenCollected += OnWithdraw;
        }
    }

    public void OnWithdraw(int amount)
    {
        if (amount > _levelPointsValueCount)
        {
            throw new IndexOutOfRangeException("Points can not be les than 0. Your value is: " + amount);
        }

        _levelPointsValueCount -= amount;
        Withdrawed?.Invoke(amount);
    }

    public void UnlinkWithdrawEvents()
    {
        for (int i = 0; i < _gamePoints.Length; i++)
        {
            _gamePoints[i].BeenCollected -= OnWithdraw;
        }
    }
}

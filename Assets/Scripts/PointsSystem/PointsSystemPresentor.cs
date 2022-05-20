using System;
using UnityEngine;

public class PointsSystemPresentor : MonoBehaviour
{
    [SerializeField] private GamePoint[] _pointsOnScene;

    private LevelPoitsPool _levelPoitsPool;

    public event Action<int> PointsWtidrawed;

    private void Awake()
    {
        _levelPoitsPool = new LevelPoitsPool(_pointsOnScene);
    }

    private void OnEnable()
    {
        _levelPoitsPool.Withdrawed += OnWithdrawPoints;
    }

    private void OnDisable()
    {
        _levelPoitsPool.UnlinkWithdrawEvents();
        _levelPoitsPool.Withdrawed -= OnWithdrawPoints;
    }


    public LevelPoitsPool GetCurrentLevelPointsPool()
    {
        return _levelPoitsPool;
    }


    private void OnWithdrawPoints(int amount)
    {
        PointsWtidrawed?.Invoke(amount);
    }
}

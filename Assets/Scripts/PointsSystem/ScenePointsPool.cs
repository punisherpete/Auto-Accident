using System;
using UnityEngine;

public class ScenePointsPool : MonoBehaviour
{
    [SerializeField] private GamePoint[] _pointsOnScene;

    public event Action<int> PointsWthidrawed;

    private void OnEnable()
    {
        for (int i = 0; i < _pointsOnScene.Length; i++)
        {
            _pointsOnScene[i].BeenCollected += OnWithdrawPoints;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _pointsOnScene.Length; i++)
        {
            _pointsOnScene[i].BeenCollected -= OnWithdrawPoints;
        }
    }

    private void OnWithdrawPoints(int amount)
    {
        PointsWthidrawed?.Invoke(amount);
    }
}

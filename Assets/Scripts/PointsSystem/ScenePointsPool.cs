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
        if (_pointsOnScene.Length > 0)
        {
            for (int i = 0; i < _pointsOnScene.Length; i++)
            {
                if (_pointsOnScene[i])
                    _pointsOnScene[i].BeenCollected -= OnWithdrawPoints;
            }
        }
    }

    private void OnWithdrawPoints(int amount)
    {
        PointsWthidrawed?.Invoke(amount);
    }
}

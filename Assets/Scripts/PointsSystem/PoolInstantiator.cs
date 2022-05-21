using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInstantiator : MonoBehaviour
{
    [SerializeField] private ScenePointsPool _scenePointsPool;

    private void Start()
    {
        PointsTransmitter.Instance.InitLevelPointsPool(_scenePointsPool);
        PointsTransmitter.Instance.Subscribe();
    }

    private void OnDisable()
    {
        PointsTransmitter.Instance.Unsubscribe();
    }
}

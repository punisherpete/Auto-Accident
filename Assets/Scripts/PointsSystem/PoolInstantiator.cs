using UnityEngine;

public class PoolInstantiator : MonoBehaviour
{
    [SerializeField] private ScenePointsPool _scenePointsPool;
    [SerializeField] private Data _data;

    private void Start()
    {
        PointsTransmitter.Instance.InitLevelPointsPool(_scenePointsPool);
        PointsTransmitter.Instance.Subscribe();
        PointsTransmitter.Instance.InitData(_data);
    }

    private void OnDisable()
    {
        PointsTransmitter.Instance.Unsubscribe();
    }
}

using System;
using UnityEngine;

public class PointsTransmitter : MonoBehaviour
{
    [SerializeField] private Data _data;

    private ScenePointsPool _pointsPool;
    private IWaletOperation _wallet;

    public static PointsTransmitter Instance;

    public event Action Transmitted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _wallet = new Wallet(_data.GetCurrentSoft());
    }

    public void Subscribe()
    {
        if (_pointsPool == null)
            return;

        _pointsPool.PointsWthidrawed += OnTransaction;
    }

    public void Unsubscribe()
    {
        if (_pointsPool == null)
            return;

        _pointsPool.PointsWthidrawed -= OnTransaction;
    }

    private void OnTransaction(int amount)
    {
        _wallet.OperateWithPoints(amount);
        Transmitted?.Invoke();
        _data.SetCurrentSoft(_wallet.GetPointsAmount());
        _data.Save();
    }

    public int GetWalletPoints()
    {
        return _wallet.GetPointsAmount();
    }

    public void DropCollectedPoints(int value)
    {
        _wallet.Reset(value);
        Transmitted?.Invoke();
        _data.SetCurrentSoft(_wallet.GetPointsAmount());
        _data.Save();
    }

    public void InitLevelPointsPool(ScenePointsPool pointsPool)
    {
        _pointsPool = pointsPool;
    }

    public void SetPoints(int value)
    {
        _wallet.OperateWithPoints(value);
    }
}

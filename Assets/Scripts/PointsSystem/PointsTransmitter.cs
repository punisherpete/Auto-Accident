using System;
using UnityEngine;

public class PointsTransmitter : MonoBehaviour
{
    private ScenePointsPool _pointsPool;
    private IWaletOperation _wallet;
    private Data _data;

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

        if(_wallet == null)
        _wallet = new Wallet();
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
        _wallet.Reset(value);
        _data.SetCurrentSoft(_wallet.GetPointsAmount());
    }

    public void InitData(Data data)
    {
        _data = data;
        if (_data)
        {
            print(_wallet.GetPointsAmount());
        }
    }
}

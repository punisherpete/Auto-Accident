using System;
using UnityEngine;

public class PointsTransmitter : MonoBehaviour
{
    [SerializeField] private int _walletPointsAmount;

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

        _wallet = new Wallet(_walletPointsAmount);
    }

    public void Subscribe()
    {
        _pointsPool.PointsWthidrawed += OnTransaction;
    }

    public void Unsubscribe()
    {
        _pointsPool.PointsWthidrawed -= OnTransaction;
    }

    private void OnTransaction(int amount)
    {
        _wallet.OperateWithPoints(amount);
        Transmitted?.Invoke();
    }

    public int GetWalletPoints()
    {
        return _wallet.GetPointsAmount();
    }

    public void DropCollectedPoints(int value)
    {
        _wallet.Reset(value);
        Transmitted?.Invoke();
    }

    public void InitLevelPointsPool(ScenePointsPool pointsPool)
    {
        _pointsPool = pointsPool;
    }
}

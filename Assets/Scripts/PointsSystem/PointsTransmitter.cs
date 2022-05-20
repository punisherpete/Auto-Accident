using UnityEngine;

public class PointsTransmitter : MonoBehaviour
{
    [SerializeField] private int _walletPointsAmount;
    [SerializeField] private PointsSystemPresentor _pointsSystemPresentor;

    private IWaletOperation _wallet;

    public static PointsTransmitter Instance;

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


    private void OnEnable()
    {
        _pointsSystemPresentor.PointsWtidrawed += OnAddPoints;
    }

    private void OnDisable()
    {
        _pointsSystemPresentor.PointsWtidrawed -= OnAddPoints;
    }

    private void OnAddPoints(int amount)
    {
        _wallet.AddPoints(amount);
    }

    public int GetWalletPoints()
    {
        return _wallet.GetPointsAmount();
    }
}

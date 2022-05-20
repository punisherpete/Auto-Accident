using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletView;
    [SerializeField] private PointsSystemPresentor _pointsSystemPresentor;

    private void OnEnable()
    {
        _pointsSystemPresentor.PointsWtidrawed += OnChangeView;
    }

    private void Start()
    {
        OnChangeView(0);
    }

    private void OnDisable()
    {
        _pointsSystemPresentor.PointsWtidrawed -= OnChangeView;
    }

    private void OnChangeView(int amount)
    {
        _walletView.text = PointsTransmitter.Instance.GetWalletPoints().ToString();
    }
}

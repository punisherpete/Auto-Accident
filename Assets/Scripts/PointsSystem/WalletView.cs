using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletView;


    private void Start()
    {
        PointsTransmitter.Instance.Transmitted += OnChangeView;
        OnChangeView();
    }

    private void OnDisable()
    {
        PointsTransmitter.Instance.Transmitted -= OnChangeView;
    }

    private void OnChangeView()
    {
        _walletView.text = PointsTransmitter.Instance.GetWalletPoints().ToString();
    }
}

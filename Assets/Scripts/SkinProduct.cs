using System;
using UnityEngine;

public class SkinProduct : MonoBehaviour
{
    [field: SerializeField] public string CodeName { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    [field: SerializeField] public Material MainCarMat;
    [field: SerializeField] public Material SecondCarMat;
    
    private PointsTransmitter _pointsTransmitter;
    public bool IsBought { get; private set; }
    public bool IsUsed { get; private set; }

    public event Action OnBought;
    public event Action OnUsed;
    public event Action OnUnused;

    private void Start()
    {
        _pointsTransmitter = (PointsTransmitter)FindObjectOfType(typeof(PointsTransmitter));
        
        if (_pointsTransmitter)
            Debug.Log("TextMesh object found: " + _pointsTransmitter.name);
        else
            Debug.Log("No TextMesh object could be found");
    }

    public event Action<SkinProduct> OnShopShouldBeUpdated;

    public void Buy()
    {
        IsBought = true;
        OnBought?.Invoke();
        
        if(_pointsTransmitter != null)
            _pointsTransmitter.Transmitter();
    }

    public void Use()
    {
        IsUsed = true;
        OnUsed?.Invoke();
        OnShopShouldBeUpdated?.Invoke(this);
    }

    public void Unuse()
    {
        IsUsed = false;
        OnUnused?.Invoke();
    }
}

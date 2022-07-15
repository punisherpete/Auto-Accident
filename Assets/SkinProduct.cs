using System;
using UnityEngine;

public class SkinProduct : MonoBehaviour
{
    [field: SerializeField] public string CodeName { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    [field: SerializeField] public Material MainCarMat;
    [field: SerializeField] public Material SecondCarMat;
    
    public bool IsBought { get; private set; }
    public bool IsUsed { get; private set; }

    public event Action OnBought;
    public event Action OnUsed;
    public event Action OnUnused;

    public void Buy()
    {
        IsBought = true;
        OnBought?.Invoke();
    }

    public void Use()
    {
        IsUsed = true;
        OnUsed?.Invoke();
    }

    public void Unuse()
    {
        IsUsed = false;
        OnUnused?.Invoke();
    }
}

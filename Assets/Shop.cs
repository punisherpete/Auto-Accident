using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shop : MonoBehaviour
{
    [SerializeField] private PlayerInteractionWithTheShop _shopInteraction;

    private List<SkinProduct> _products = new List<SkinProduct>();

    private void Start()
    {
        _products = GetComponentsInChildren<SkinProduct>().ToList();
    }

    public void BuyProduct(SkinProduct skin)
    {
        _shopInteraction.TryBuyProduct(skin);
    }
}

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
        
        foreach (var product in _products)
        {
            product.OnShopShouldBeUpdated += UpdateShop;
            product.OnShopShouldBeUpdated += UseProduct;
        }
    }

    public void BuyProduct(SkinProduct skin)
    {
        if (_shopInteraction.TryBuyProduct(skin))
        {
            skin.Buy();
            skin.Use();
        }
    }

    public void UseProduct(SkinProduct skin)
    {
        _shopInteraction.UseProduct(skin);
    }

    public void UpdateShop(SkinProduct skin)
    {
        foreach (var product in _products)
        {
            if (product.IsBought && product != skin)
                product.Unuse();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsLoader : MonoBehaviour
{
    [SerializeField] private List<ProductSaveLoad> _products;
    [SerializeField] private List<ProductView> _productViews;
    [SerializeField] private List<SkinProduct> _produts;

    private void Start()
    {
        for (int i = 0; i < _productViews.Count; i++)
        {
            _productViews[i].Initialize(_produts[i]);
        }

        foreach (var product in _products)
        {
            product.Initialize();
        }
    }
}

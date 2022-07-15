using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SkinProduct))]

public class ProductView : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _usedButton;
    [SerializeField] private GameObject _unusedButton;

    private SkinProduct _product;

    private void Start()
    {
        _product = GetComponent<SkinProduct>();
        _product.OnBought += Buy;
        _product.OnUsed += Use;
        _product.OnUnused += Unuse;


        _priceText.text = _product.Price.ToString();
    }

    private void Buy()
    {
        _buyButton.SetActive(false);
    }

    private void Use()
    {
        _buyButton.SetActive(false);
        _unusedButton.SetActive(false);
        _usedButton.SetActive(true);
    }

    private void Unuse()
    {
        _buyButton.SetActive(false);
        _unusedButton.SetActive(true);
        _usedButton.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinProduct))]
public class ProductSaveLoad : MonoBehaviour
{
    [SerializeField] private SkinProduct _skin;
    [SerializeField] private Shop _shop;

    private string _itemUsedSavePath;
    private string _itemBoughtSavePath;

    public void Initialize()
    {
        _itemUsedSavePath = _skin.CodeName + "_used";
        _itemBoughtSavePath = _skin.CodeName + "_bought";

        Load();

        _skin.OnUsed += Save;
        _skin.OnUnused += Save;
    }

    public void Save()
    {
        // 0 for not bought, 1 for bought
        PlayerPrefs.SetInt(_itemBoughtSavePath, _skin.IsBought ? 1 : 0);
        // 0 for not used, 1 for used
        PlayerPrefs.SetInt(_itemUsedSavePath, _skin.IsUsed ? 1 : 0);
    }

    public void Load()
    {
        bool isBought = PlayerPrefs.GetInt(_itemBoughtSavePath) == 1;
        bool isUsed = PlayerPrefs.GetInt(_itemUsedSavePath) == 1;

        if(isBought)
        {
            if (isUsed)
                _shop.BuyProduct(_skin);
            else
            {
                _skin.Buy();
                _skin.Unuse();
            } 
        }
    }
}

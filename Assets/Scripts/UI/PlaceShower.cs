using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Localization;

public class PlaceShower : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private Car _determinedCar;
    [SerializeField] private TMP_Text _placeText;

    private void FixedUpdate()
    {
        _placeText.text = LeanLocalization.GetTranslationText("Place") + ": " + _carsObserver.DetermineCurrentPlace(_determinedCar).ToString();
    }
}

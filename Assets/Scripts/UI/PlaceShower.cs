using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceShower : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private Car _determinedCar;
    [SerializeField] private TMP_Text _placeText;

    private void FixedUpdate()
    {
        _placeText.text = _carsObserver.DetermineCurrentPlace(_determinedCar).ToString();
    }
}

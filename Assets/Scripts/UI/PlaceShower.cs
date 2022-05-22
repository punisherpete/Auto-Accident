using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceShower : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private Car _determinedCar;

    private void FixedUpdate()
    {
        Debug.Log(_carsObserver.DetermineCurrentPlace(_determinedCar));
    }
}

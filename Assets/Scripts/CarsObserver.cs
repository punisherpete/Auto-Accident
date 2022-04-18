using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsObserver : MonoBehaviour
{
    private List<Car> _cars;

    private void Awake()
    {
        _cars = new List<Car>(GetComponentsInChildren<Car>());
    }

    public bool IsCarInSafeZone(Transform originCar, float safeDistanceForSafeMode)
    {
        foreach (var car in _cars)
        {
            if (car.transform == originCar)
                continue;
            if (Vector3.Distance(originCar.position, car.transform.position) < safeDistanceForSafeMode)
                return false;
        }
        return true;
    }

    public void StopAllCars()
    {
        foreach (var car in _cars)
        {
            car.StopMashine();
        }
    }
}

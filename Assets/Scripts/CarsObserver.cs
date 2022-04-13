using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsObserver : MonoBehaviour
{
    private List<Mover> _carMovers;

    private void Awake()
    {
        _carMovers = new List<Mover>(GetComponentsInChildren<Mover>());
    }

    public bool IsCarInSafeZone(Transform originCar, float safeDistanceForSafeMode)
    {
        foreach (var item in _carMovers)
        {
            if (item.transform == originCar)
                continue;
            if (Vector3.Distance(originCar.position, item.transform.position) < safeDistanceForSafeMode)
                return false;
        }
        return true;
    }
}

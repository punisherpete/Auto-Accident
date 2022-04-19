using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;

    private SpeedLimit _speedLimit;

    private void OnEnable()
    {
        _speedLimit = GetComponent<SpeedLimit>();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _criticalDistanceFromPlayer = 40;
    [SerializeField] private float _dragModifier = 0.05f;

    private SpeedLimit _speedLimit;

    private void OnEnable()
    {
        _speedLimit = GetComponent<SpeedLimit>();
    }

    private void FixedUpdate()
    {
        if (_carsObserver.IsExceedsCriticalDistanceFromPlayer(transform, _criticalDistanceFromPlayer)) 
            _speedLimit.SetRegularDragForce(_dragModifier);
        else
            _speedLimit.SetRegularDragForce(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _criticalDistanceFromPlayer = 10;
    [SerializeField] private float _dragModifier = 0.005f;
    [SerializeField] private float _maxSpeedModifier = 1.1f;

    private SpeedLimit _speedLimit;
    private Mover _mover;

    private void OnEnable()
    {
        _speedLimit = GetComponent<SpeedLimit>();
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        if (_carsObserver.IsAheadOfThePlayerOnDistance(transform, _criticalDistanceFromPlayer)) 
            _speedLimit.SetRegularDragForce(_dragModifier);
        else
            _speedLimit.SetRegularDragForce(0);
        if (_carsObserver.IsFallBehindOfThePlayerOnDistance(transform, _criticalDistanceFromPlayer))
            _mover.SetMaxSpeedModifier(_maxSpeedModifier);
        else
            _mover.SetMaxSpeedModifier(1);
    }
}

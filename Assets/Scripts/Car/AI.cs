using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _criticalLeadDistanceFromPlayer = 10;
    [SerializeField] private float _criticalBehindDistanceFromPlayer = 10;
    [SerializeField] private float _dragModifier = 0.005f;
    [SerializeField] private float _maxSpeedModifier = 1.1f;
    [SerializeField] private float _regularForce = 5000f;

    private SpeedLimit _speedLimit;
    private Mover _mover;

    private void OnEnable()
    {
        _speedLimit = GetComponent<SpeedLimit>();
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        if (_carsObserver.IsAheadOfThePlayerOnDistance(transform, _criticalLeadDistanceFromPlayer)) 
            _speedLimit.SetRegularDragForce(_dragModifier);
        else
            _speedLimit.SetRegularDragForce(0);
        if (_carsObserver.IsFallBehindOfThePlayerOnDistance(transform, _criticalBehindDistanceFromPlayer))
        {
            _mover.SetMaxSpeedModifier(_maxSpeedModifier,_regularForce);
            _mover.StrengthenWheels();
        }
        else
        {
            _mover.SetMaxSpeedModifier(1,0);
            _mover.ResetToDefaultWheel();
        }
    }
}

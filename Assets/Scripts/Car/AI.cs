using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _criticalLeadDistanceFromPlayer = 10;
    [SerializeField] private float _criticalBehindDistanceFromPlayer = 24;
    [SerializeField] private float _cheaterBehindDistanceFromPlayer = 54;
    [SerializeField] private float _dragModifier = 0.005f;
    [SerializeField] private float _behindSpeedModifier = 1.3f;
    [SerializeField] private float _cheaterSpeedModifier = 1.5f;
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
        if (_carsObserver.IsFallBehindOfThePlayerOnDistance(transform, _cheaterBehindDistanceFromPlayer))
        {
            _mover.SetMaxSpeedModifier(_cheaterSpeedModifier, _regularForce);
            _mover.StrengthenWheels();
        }
        else if(_carsObserver.IsFallBehindOfThePlayerOnDistance(transform, _criticalBehindDistanceFromPlayer))
        {
            _mover.SetMaxSpeedModifier(_behindSpeedModifier, 0);
            _mover.StrengthenWheels();
        }
        else
        {
            _mover.SetMaxSpeedModifier(1,0);
            _mover.ResetToDefaultWheel();
        }
    }
}

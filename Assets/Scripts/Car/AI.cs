using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _cheaterLeadDistanceFromPlayer = 125;
    [SerializeField] private float _strongLeadDistanceFromPlayer = 50;
    [SerializeField] private float _criticalLeadDistanceFromPlayer = 10;
    [SerializeField] private float _criticalBehindDistanceFromPlayer = 24;
    [SerializeField] private float _cheaterBehindDistanceFromPlayer = 54;
    [SerializeField] private float _dragModifier = 0.005f;
    [SerializeField] private float _strongDragModifier = 0.025f;
    [SerializeField] private float _impossibleDragModifier = 0.3f;
    [SerializeField] private float _behindSpeedModifier = 1.3f;
    [SerializeField] private float _cheaterSpeedModifier = 1.5f;
    [SerializeField] private float _regularForce = 5000f;
    [SerializeField] private float _slidingTime = 2f;
    [SerializeField] private float _criticalSpeedDifference = 4f;

    private SpeedLimit _speedLimit;
    private Mover _mover;
    private Car _car;
    private bool _isAiStrong = true;

    private void OnEnable()
    {
        _speedLimit = GetComponent<SpeedLimit>();
        _mover = GetComponent<Mover>();
        _car = GetComponent<Car>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Car car))
        {
            if (car.Type == CarType.Player)
                _car.SetSlidingWheel(_slidingTime);
        }
    }

    public void DeactivateStrongAI()
    {
        _isAiStrong = false;
    }

    private void FixedUpdate()
    {
        if(_carsObserver.IsAheadOfThePlayerOnDistance(_car, _cheaterLeadDistanceFromPlayer))
            _speedLimit.SetRegularDragForce(_impossibleDragModifier);
        else if (_carsObserver.IsAheadOfThePlayerOnDistance(_car, _strongLeadDistanceFromPlayer))
            _speedLimit.SetRegularDragForce(_strongDragModifier);
        else if (_carsObserver.IsAheadOfThePlayerOnDistance(_car, _criticalLeadDistanceFromPlayer) && _carsObserver.IsPlayerLeadsActiveGame)
        {
            if(_carsObserver.IsCarFasterThanPlayer(_car, _criticalSpeedDifference))
                _speedLimit.SetRegularDragForce(_strongDragModifier);
            else
                _speedLimit.SetRegularDragForce(_dragModifier);
        }
        else
            _speedLimit.SetRegularDragForce(0);
        if (_carsObserver.IsFallBehindOfThePlayerOnDistance(_car, _cheaterBehindDistanceFromPlayer) && _isAiStrong)
        {
            _mover.SetMaxSpeedModifier(_cheaterSpeedModifier, _regularForce);
            _mover.StrengthenWheels();
        }
        else if (_carsObserver.IsFallBehindOfThePlayerOnDistance(_car, _criticalBehindDistanceFromPlayer) && _isAiStrong)
        {
            _mover.SetMaxSpeedModifier(_behindSpeedModifier, 0);
            _mover.StrengthenWheels();
        }
        else
        {
            _mover.SetMaxSpeedModifier(1, 0);
            _mover.TryResetToDefaultWheel();
        }
    }
}

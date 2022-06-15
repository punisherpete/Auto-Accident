using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpeedLimit))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Car))]
public class AI : MonoBehaviour
{
    [SerializeField] private CarsObserver _observer;

    [SerializeField] private float _cheaterLeadDistance = 125;
    [SerializeField] private float _strongLeadDistance = 50;
    [SerializeField] private float _criticalLeadDistance = 10;
    [SerializeField] private float _criticalBehindDistance = 24;
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

    private bool _isStrong = true;

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

    public void BecomeWeak()
    {
        _isStrong = false;
    }

    private void FixedUpdate()
    {
        _speedLimit.SetRegularDragForce(CalculateDragForce());
        DetermineSpeed();
    }

    private float CalculateDragForce()
    {
        float? distance = _observer.DistanceAheadOfPlayer(_car);

        if (distance == null)
            return 0;

        if (distance > _cheaterLeadDistance)
            return _impossibleDragModifier;
        else if (distance > _strongLeadDistance)
            return _strongDragModifier;
        else if (distance > _criticalLeadDistance && _observer.IsPlayerActive)
            return _observer.IsFasterThanPlayer(_car, _criticalSpeedDifference)
                ? _strongDragModifier : _dragModifier;

        return 0;
    }

    private void DetermineSpeed()
    {
        float? distance = _observer.DistanceBehindThePlayer(_car);

        if (distance == null)
            return;

        if (_isStrong)
        {
            if (distance > _cheaterBehindDistanceFromPlayer)
                _mover.SetMaxSpeedModifier(_cheaterSpeedModifier, _regularForce);
            else if (distance > _criticalBehindDistance)
                _mover.SetMaxSpeedModifier(_behindSpeedModifier, 0);

            _mover.StrengthenWheels();
            return;
        }

        _mover.SetMaxSpeedModifier(1, 0);
        _mover.TryResetToDefaultWheel();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpeedLimit))]
public class Transmission : MonoBehaviour
{
    [SerializeField] private List<Transfer> _transfers;
    [SerializeField] private bool _isNeutral;

    private int _currentTransferIndex;
    private float _acceleration;
    private float _maxSpeed;
    private Rigidbody _rigidbody;
    private SpeedLimit _speedLimit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speedLimit = GetComponent<SpeedLimit>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude < _transfers[_currentTransferIndex].MinSpeed || _transfers[_currentTransferIndex].MaxSpeed  > _maxSpeed)
            IncreaseTransmission();
        else if (_rigidbody.velocity.magnitude > _transfers[_currentTransferIndex].MaxSpeed )
            ReduceTransmission();
        _speedLimit.LimitedSpeed(_transfers[_currentTransferIndex].MaxSpeed);
        _acceleration = Mathf.Lerp(_acceleration, _transfers[_currentTransferIndex].Acceleration, Time.fixedDeltaTime);
    }

    public void TurnOnNeutral()
    {
        _isNeutral = true;
    }

    public void TurnOnDrive()
    {
        _isNeutral = false;
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        _maxSpeed = maxSpeed;
    }

    public float GetAcceleration()
    {
        if (_isNeutral)
            return 0;
        return _acceleration;
    }

    public float GetForce()
    {
        if (_isNeutral)
            return 0;
        return _transfers[_currentTransferIndex].Force;
    }

    private void ReduceTransmission()
    {
        if (_currentTransferIndex < _transfers.Count - 1)
        {
            if (_transfers[_currentTransferIndex].MaxSpeed < _maxSpeed)
                _currentTransferIndex++;
        }
    }

    private void IncreaseTransmission()
    {
        if (_currentTransferIndex > 0)
            _currentTransferIndex--;
    }
}

[Serializable]
public class Transfer
{
    public float MinSpeed;
    public float MaxSpeed;
    public float Acceleration;
    public float Force;
}


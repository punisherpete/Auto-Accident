using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpeedLimit))]
public class Transmission : MonoBehaviour
{
    [SerializeField] private List<Transfer> _transfers;
    [SerializeField] private int _transferIndex;
    [SerializeField] private bool _isNeutral;
    

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
        Debug.LogWarning(_rigidbody.velocity.magnitude + " " + _maxSpeed);
        if(_rigidbody.velocity.magnitude < _transfers[_transferIndex].MinSpeed || _transfers[_transferIndex].MaxSpeed > _maxSpeed)
            IncreaseTransmission();
        else if(_rigidbody.velocity.magnitude>_transfers[_transferIndex].MaxSpeed)
            ReduceTransmission(); 
        _speedLimit.LimitedSpeed(_transfers[_transferIndex].MaxSpeed);
        _acceleration = Mathf.Lerp(_acceleration, _transfers[_transferIndex].Acceleration, Time.fixedDeltaTime);
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
        if(_isNeutral)
            return 0;
        return _acceleration;
    }

    private void ReduceTransmission()
    {
        if (_transferIndex < _transfers.Count-1)
        {
            if (_transfers[_transferIndex].MaxSpeed < _maxSpeed)
                _transferIndex++;
        }
    }

    private void IncreaseTransmission()
    {
        if (_transferIndex > 0)
            _transferIndex--;
    }
}

[Serializable]
public class Transfer
{
    public float MinSpeed;
    public float MaxSpeed;
    public float Acceleration;
}


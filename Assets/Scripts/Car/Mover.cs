using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transmission))]
[RequireComponent(typeof(PathController))]
[RequireComponent(typeof(WheelController))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _criticalOffset = 5f;
    [SerializeField] private float _offsetSpeed = 3f;
    [SerializeField] private float _maxSpeed = 30f;
    [SerializeField] private float _motorForce = 100f;
    [SerializeField] private float _breakForce = 1000f;
    [SerializeField] private float _maxSteerAngle = 35f;
    [SerializeField] private float _centerOfMass = -.5f;
    [SerializeField] private float _airRotationSensitivity = 5f;
    [SerializeField] private float _airMovementSensitivity = 5f;

    private PathController _pathController;
    private float _currentSteerAngle;
    private float _currentBreakForce;
    private bool _isStop;
    private float _currentRotationWheel = 0;
    private float _breakingTimer = 0f;
    private WheelController _wheelController;
    private Transmission _transmission;
    private float _boostAccelerationModifier = 1f;
    private float _boostSpeedModifier = 1f;
    private Rigidbody _rigidbody;
    private float _maxSpeedModifier = 1f;
    private float _slidingTime;
    private bool _changeOffsetPermission = true;
    private bool _rotatePermission = true;
    private Quaternion _startRotation;
    private bool _hasContactsInArea = true;

    public float CurrentRotationWheel => _currentRotationWheel;
    public bool RotatePermission => _rotatePermission;
    public float MaxSpeed => _maxSpeed;
    public bool IsStop => _isStop;
    public float SlidingTime => _slidingTime;
    public bool IsOnGround => _wheelController.IsGrounded;
    public bool IsAllWheelsOnGround => _wheelController.IsAllWheelsOnGround;

    public event Action Boosted;

    private void Awake()
    {
        _wheelController = GetComponent<WheelController>();
        _transmission = GetComponent<Transmission>();
        _pathController = GetComponent<PathController>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = Vector3.up * _centerOfMass;
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (_breakingTimer > 0)
            _breakingTimer -= Time.fixedDeltaTime;
        HandleMotor();
        HandleSteering();
        if (_slidingTime > 0)
        {
            _slidingTime -= Time.fixedDeltaTime;
            _wheelController.SetSlidingWheelFrictionCurve();
            if (_slidingTime <= 0)
                _wheelController.ResetWheelFrictionCurve();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _hasContactsInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _hasContactsInArea = false;
    }

    public void SetCriticalHorizontalOffset(float horizontalOffset)
    {
        _criticalOffset = horizontalOffset;
    }

    public void StrengthenWheels()
    {
        _wheelController.ReinfoceWheelFrictionCurve();
    }

    public void SetSlidingWheel(float time)
    {
        _slidingTime = time;
    }

    public void AllowChangeOffset()
    {
        _changeOffsetPermission = true;
    }

    public void ProhibitChangeOffset()
    {
        _changeOffsetPermission = false;
    }

    public bool TryResetToDefaultWheel()
    {
        if (_slidingTime <= 0)
        {
            _wheelController.ResetWheelFrictionCurve();
            return true;
        }
        return false;
    }

    public void HandleCarInAir(float joysticHorizontalInput)
    {
        if (_hasContactsInArea == false)
        {

            Quaternion targetRotation = Quaternion.LookRotation(transform.position, new Vector3(0, _pathController.TargetPoint.position.y, 0));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
            Rotate(joysticHorizontalInput);
            Drag(joysticHorizontalInput);
        }
    }

    public void Rotate(float joysticHorizontal)
    {
        if (!_rotatePermission)
            return;
        Vector3 newRotation = Vector3.up * joysticHorizontal * Time.deltaTime * _airRotationSensitivity;
        _rigidbody.AddTorque(newRotation, ForceMode.VelocityChange);
    }

    public void Drag(float joysticHorizontal)
    {
        Vector3 dragForce = Vector3.left * joysticHorizontal * Time.deltaTime * _airMovementSensitivity;
        _rigidbody.AddForce(dragForce, ForceMode.VelocityChange);

    }

    public void ProhibitRotationForAWhile(float time)
    {
        StopCoroutine(ActivateRotateAfterAWhile(time));
        _rotatePermission = false;
        StartCoroutine(ActivateRotateAfterAWhile(time));
    }

    public void TryChangeHorizontalOffset(float horizontalInput)
    {
        if (_changeOffsetPermission && _wheelController.IsGrounded)
            _pathController.ChangeHorizontalOffset(_criticalOffset, _offsetSpeed, horizontalInput);
    }

    public void PauseMoving(float time)
    {
        _breakingTimer = time;
    }

    public void SetMaxSpeedModifier(float modifier, float regularForce)
    {
        _maxSpeedModifier = modifier;
        _transmission.SetRegularForce(regularForce);
        SetMaxSpeed(_maxSpeed);
    }

    public void StopMoving()
    {
        _isStop = true;
        _transmission.TurnOnNeutral();
    }

    public void StartMoving()
    {
        _isStop = false;
        _transmission.SetMaxSpeed(_maxSpeed);
        _transmission.TurnOnDrive();
    }

    public void SetOffsetSpeed(float speed)
    {
        _offsetSpeed = speed;
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        _maxSpeed = maxSpeed;
        _transmission.SetMaxSpeed(_maxSpeed * _boostSpeedModifier * _maxSpeedModifier);
    }

    public void SetBoost(float boostAccelerationValue, float boostSpeedValue, float boostImpulseForce, float boostTime, Vector3 direction)
    {
        Boost(boostAccelerationValue, boostSpeedValue, boostImpulseForce, boostTime, direction);
    }

    public void SetBoost(float boostAccelerationValue, float boostSpeedValue, float boostImpulseForce, float boostTime)
    {
        Boost(boostAccelerationValue, boostSpeedValue, boostImpulseForce, boostTime, transform.forward);
    }

    private void Boost(float boostAccelerationValue, float boostSpeedValue, float boostImpulseForce, float boostTime, Vector3 direction)
    {
        Boosted?.Invoke();
        _boostSpeedModifier = boostSpeedValue;
        _boostAccelerationModifier = boostAccelerationValue;
        SetMaxSpeed(_maxSpeed);
        _rigidbody.AddForce(direction * boostImpulseForce, ForceMode.Impulse);
        StartCoroutine(ResetBoostValues(boostTime));
    }

    public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.magnitude;
    }

    public float GetMaxSpeed()
    {
        return _maxSpeed * _boostSpeedModifier * _maxSpeedModifier;
    }

    public float GetAcceleration()
    {
        return _transmission.GetAcceleration() * _boostAccelerationModifier;
    }

    public float GetCriticalOffset()
    {
        return _criticalOffset;
    }

    private void HandleMotor()
    {
        _wheelController.SetForce(_transmission.GetAcceleration() * _motorForce * _boostAccelerationModifier);
        if (_wheelController.IsGrounded)
            _rigidbody.AddForce(transform.forward * _transmission.GetForce());
        if (_isStop)
            _currentBreakForce = _breakForce;
        else
            _currentBreakForce = _breakingTimer > 0 ? _breakForce : 0f;
        _wheelController.ApplyBreaking(_currentBreakForce);
    }

    private void HandleSteering()
    {
        Vector3 realitiveVector = transform.InverseTransformPoint(_pathController.TargetPoint.position);
        realitiveVector = realitiveVector / realitiveVector.magnitude;
        float rotationToTargetSample = realitiveVector.x / realitiveVector.magnitude;
        _currentRotationWheel = rotationToTargetSample;/*Mathf.Lerp(_currentRotationWheel, rotationToTargetSample, _turningPower * Time.fixedDeltaTime);*/
        _currentSteerAngle = _maxSteerAngle * _currentRotationWheel;
        _wheelController.SetSeetAngle(_currentSteerAngle);
    }

    private IEnumerator ResetBoostValues(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _boostSpeedModifier = 1;
        _boostAccelerationModifier = 1;
        SetMaxSpeed(_maxSpeed);
    }

    private IEnumerator ActivateRotateAfterAWhile(float time)
    {
        yield return new WaitForSeconds(time);
        _rotatePermission = true;
    }
}

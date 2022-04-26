using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transmission))]
public class Mover : MonoBehaviour
{

    [SerializeField] private float _criticalOffset = 2f;
    [SerializeField] private float _offsetSpeed = 2f;
    [SerializeField] private float _maxSpeed = 30f;
    [SerializeField] private float _motorForce = 100f;
    [SerializeField] private float _breakForce = 1000f;
    [SerializeField] private float _maxSteerAngle = 35f;
    [SerializeField] private float _turningPower = 20f;
    [SerializeField] private float _centerOfMass = -.5f;

    private PathMover _pathController = null;
    private float _currentSteerAngle;
    private float _currentBreakForce;
    private bool _isStop;
    private Transform _targetNode;
    private Transform _currentNode;
    private float _currentRotationWheel = 0;
    private float _breakingTimer = 0f;
    private WheelController _wheelController;
    private Transmission _transmission;
    private float _boostAccelerationModifier = 1f;
    private float _boostSpeedModifier = 1f;
    private Rigidbody _rigidbody;
    private float _maxSpeedModifier = 1f;

    public float CurrentRotationWheel => _currentRotationWheel;
    public float MaxSpeed => _maxSpeed;
    public Transform CurrentNode => _currentNode;
    public bool IsStop => _isStop;

    private void Awake()
    {
        _wheelController = GetComponent<WheelController>();
        _transmission = GetComponent<Transmission>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = Vector3.up * _centerOfMass;
    }

    private void FixedUpdate()
    {
        if (_targetNode == null || _pathController == null)
            return;
        if (_breakingTimer > 0)
            _breakingTimer -= Time.fixedDeltaTime;
        HandleMotor();
        HandleSteering();
    }

    public void SetPathController(PathMover pathController)
    {
        _pathController = pathController;
    }

    public void SetTargetNode(Transform currentNode,Transform targetNode)
    {
        _currentNode = currentNode;
        _targetNode = targetNode;
    }

    public void SetCriticalHorizontalOffset(float horizontalOffset)
    {
        _criticalOffset = horizontalOffset;
    }

    public void StrengthenWheels()
    {
        _wheelController.ReinfoceWheelFrictionCurve();
    }

    public void ResetToDefaultWheel()
    {
        _wheelController.ResetWheelFrictionCurve();
    }

    public void ChangeHorizontalOffset(float horizontalInput)
    {
        _pathController.MovePath(_criticalOffset,_offsetSpeed,horizontalInput);
    }

    public void PauseMoving(float time)
    {
        _breakingTimer = time;
    }

    public void SetMaxSpeedModifier(float modifier)
    {
        _maxSpeedModifier = modifier;
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

    public void SetMaxSpeed(float value)
    {
        _maxSpeed = value;
        _transmission.SetMaxSpeed(_maxSpeed * _boostSpeedModifier * _maxSpeedModifier);
    }

    public void SetBoost(float boostAccelerationValue, float boostSpeedValue,float boostImpulseForce, float boostTime)
    {
        _boostSpeedModifier = boostSpeedValue;
        _boostAccelerationModifier = boostAccelerationValue;
        SetMaxSpeed(_maxSpeed);
        _rigidbody.AddForce(transform.forward * boostImpulseForce, ForceMode.Impulse);
        StartCoroutine(ResetBoostValues(boostTime));
    }

    public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.magnitude;
    }

    public float GetMaxSpeed()
    {
        return _maxSpeed * _boostSpeedModifier;
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
        if(_isStop)
            _currentBreakForce = _breakForce;
        else
            _currentBreakForce = _breakingTimer > 0 ? _breakForce : 0f;
        _wheelController.ApplyBreaking(_currentBreakForce);
    }

    private void HandleSteering()
    {
        Vector3 realitiveVector = transform.InverseTransformPoint(_targetNode.position);
        realitiveVector = realitiveVector / realitiveVector.magnitude;
        float rotationToTargetSample = realitiveVector.x / realitiveVector.magnitude;
        _currentRotationWheel = Mathf.Lerp(_currentRotationWheel, rotationToTargetSample, _turningPower * Time.fixedDeltaTime);
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
}

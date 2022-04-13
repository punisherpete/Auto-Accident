using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField] private float _criticalOffset = 2f;
    [SerializeField] private float _offsetSpeed = 2f;
    [SerializeField] private float _currentSpeed = 1f;

    [SerializeField] private float _motorForce = 100f;
    [SerializeField] private float _breakForce = 1000f;
    [SerializeField] private float _maxSteerAngle = 35f;
    [SerializeField] private float _turningPower = 20f;
    [Header("Wheel Collider")]
    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider;
    [SerializeField] private WheelCollider _rearRightWheelCollider;
    [Header("Wheel Tranform")]
    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _frontRightWheeTransform;
    [SerializeField] private Transform _rearLeftWheelTransform;
    [SerializeField] private Transform _rearRightWheelTransform;
    
    private PathMover _pathController = null;
    private float _currentSteerAngle;
    private float _currentbreakForce;
    private bool _isStop;
    private Transform _targetNode;
    private Transform _currentNode;
    private float _currentRotationWheel = 0;
    private float _horizontalOffset;
    private float _breakingTimer = 0f;

    public float CurrentRotationWheel => _currentRotationWheel;
    public Transform CurrentNode => _currentNode;
    public bool IsStop => _isStop;


    private void FixedUpdate()
    {
        if (_targetNode == null || _pathController == null)
            return;
        if (_breakingTimer > 0)
            _breakingTimer -= Time.fixedDeltaTime;
        HandleMotor();
        HandleSteering();
        UpdateWheels();
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

    public void ChangeHorizontalOffset(float horizontalInput)
    {
        if (_horizontalOffset > _criticalOffset)
            _horizontalOffset -= _offsetSpeed * Time.fixedDeltaTime;
        else if (_horizontalOffset < -_criticalOffset)
            _horizontalOffset += _offsetSpeed * Time.fixedDeltaTime;
        else
            _horizontalOffset += horizontalInput * _offsetSpeed * Time.fixedDeltaTime;
    }

    private void HandleMotor()
    {
        _frontLeftWheelCollider.motorTorque = _currentSpeed * _motorForce;
        _frontRightWheelCollider.motorTorque = _currentSpeed * _motorForce;
        _currentbreakForce = _isStop ? _breakForce : 0f;
        _currentbreakForce = _breakingTimer > 0 ? _breakForce : 0f;
        ApplyBreaking();
    }

    public void PauseMoving(float time)
    {
        Debug.LogWarning("Break");
        _breakingTimer = time;
    }

    public void StopMoving()
    {
        Debug.LogError("Stop");
        _isStop = true;
    }

    public void StartMoving()
    {
        _isStop = false;
    }

    private void ApplyBreaking()
    {
        _frontRightWheelCollider.brakeTorque = _currentbreakForce;
        _frontLeftWheelCollider.brakeTorque = _currentbreakForce;
        _rearLeftWheelCollider.brakeTorque = _currentbreakForce;
        _rearRightWheelCollider.brakeTorque = _currentbreakForce;
    }

    private void HandleSteering()
    {
        _pathController.MovePath(_horizontalOffset);
        Vector3 realitiveVector = transform.InverseTransformPoint(_targetNode.position);
        realitiveVector = realitiveVector / realitiveVector.magnitude;
        float rotationToTargetSample = realitiveVector.x / realitiveVector.magnitude;
        _currentRotationWheel = Mathf.Lerp(_currentRotationWheel, rotationToTargetSample, _turningPower * Time.fixedDeltaTime);
        _currentSteerAngle = _maxSteerAngle * _currentRotationWheel;
        _frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        _frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheel(_frontRightWheelCollider, _frontRightWheeTransform);
        UpdateSingleWheel(_rearRightWheelCollider, _rearRightWheelTransform);
        UpdateSingleWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}

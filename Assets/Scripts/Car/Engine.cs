using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{

    private float _horizontalInput;
    private float _currentSteerAngle;
    private float _currentbreakForce;
    private bool _isBreaking;

    [SerializeField] private float _offsetSpeed = 2f;
    [SerializeField] private float _currentSpeed = 1f;
    [SerializeField] [Range(-1f,1f)] private float _targetHorizontalOffset;

    [SerializeField] private float _motorForce = 100f;
    [SerializeField] private float _breakForce = 1000f;
    [SerializeField] private float _maxSteerAngle = 35f;
    [SerializeField] private float _turningPower = 20f;

    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider;
    [SerializeField] private WheelCollider _rearRightWheelCollider;

    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _frontRightWheeTransform;
    [SerializeField] private Transform _rearLeftWheelTransform;
    [SerializeField] private Transform _rearRightWheelTransform;

    [Header("Optional")]
    [SerializeField] private PathMover _pathController = null;

    private Transform _targetNode;
    private float _currentRotationWheel = 0;

    private void FixedUpdate()
    {
        if (_targetNode == null || _pathController == null)
            return;
        /*GetInput();*/
        _horizontalInput = Mathf.Lerp(_horizontalInput, _targetHorizontalOffset, _offsetSpeed * Time.fixedDeltaTime);
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    public void SetPathController(PathMover pathController)
    {
        _pathController = pathController;
    }

    public void SetTargetNode(Transform node)
    {
        _targetNode = node;
    }

    public void SetTargetHorizontalInput(float targetHorizontalInput)
    {
        _targetHorizontalOffset = targetHorizontalInput;
    }

    /*private void GetInput()
    {
        if (Input.GetKey(KeyCode.A) && _horizontalInput > -1)
            _horizontalInput -= _offsetSpeed * Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.D) && _horizontalInput < 1)
            _horizontalInput += _offsetSpeed * Time.fixedDeltaTime;
        _isBreaking = Input.GetKey(KeyCode.Space);
    }*/

    private void HandleMotor()
    {
        _frontLeftWheelCollider.motorTorque = _currentSpeed * _motorForce;
        _frontRightWheelCollider.motorTorque = _currentSpeed * _motorForce;
        _currentbreakForce = _isBreaking ? _breakForce : 0f;
        ApplyBreaking();
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
        _pathController.MovePath(_horizontalInput);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
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
    [Header("Reinforced Wheels")]
    [SerializeField] private float _reinforcedExtremumValue;
    [SerializeField] private float _reinforcedStiffness;

    private WheelFrictionCurve _reinforcedWheelFrictionCurve;
    private WheelFrictionCurve _defaultWheelFrictionCurve;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _defaultWheelFrictionCurve = _frontLeftWheelCollider.sidewaysFriction;
        _reinforcedWheelFrictionCurve = _defaultWheelFrictionCurve;
        // _reinforcedWheelFrictionCurve.stiffness = _reinforcedStiffness;
        // _reinforcedWheelFrictionCurve.extremumValue = _reinforcedExtremumValue;
    }

    private void FixedUpdate()
    {
        UpdateWheels();
        _isGrounded = _frontLeftWheelCollider.isGrounded || _frontRightWheelCollider.isGrounded;
    }

    public void ReinfoceWheelFrictionCurve()
    {
        // _frontLeftWheelCollider.sidewaysFriction = _reinforcedWheelFrictionCurve;
        // _rearLeftWheelCollider.sidewaysFriction = _reinforcedWheelFrictionCurve;
        // _frontRightWheelCollider.sidewaysFriction = _reinforcedWheelFrictionCurve;
        // _rearRightWheelCollider.sidewaysFriction = _reinforcedWheelFrictionCurve;
    }

    public void ResetWheelFrictionCurve()
    {
        // _frontLeftWheelCollider.sidewaysFriction = _defaultWheelFrictionCurve;
        // _rearLeftWheelCollider.sidewaysFriction = _defaultWheelFrictionCurve;
        // _frontRightWheelCollider.sidewaysFriction = _defaultWheelFrictionCurve;
        // _rearRightWheelCollider.sidewaysFriction = _defaultWheelFrictionCurve;
    }

    public Vector3 GetDirectionOfWheels()
    {
        return _frontLeftWheelCollider.transform.forward;
    }

    public void SetSeetAngle(float steerAngle)
    {
        _frontLeftWheelCollider.steerAngle = steerAngle;
        _frontRightWheelCollider.steerAngle = steerAngle;
    }

    public void ApplyBreaking(float brakeForce)
    {
        _frontRightWheelCollider.brakeTorque = brakeForce;
        _frontLeftWheelCollider.brakeTorque = brakeForce;
        _rearLeftWheelCollider.brakeTorque = brakeForce;
        _rearRightWheelCollider.brakeTorque = brakeForce;
    }

    public void SetForce(float force)
    {
        _frontLeftWheelCollider.motorTorque = force;
        _frontRightWheelCollider.motorTorque = force;
    }

    public void EnableWheelColliders()
    {
        _frontLeftWheelCollider.enabled = true;
        _frontRightWheelCollider.enabled = true;
        _rearLeftWheelCollider.enabled = true;
        _rearRightWheelCollider.enabled = true;
    }

    public void DisableWheelColliders()
    {
        _frontLeftWheelCollider.enabled = false;
        _frontRightWheelCollider.enabled = false;
        _rearLeftWheelCollider.enabled = false;
        _rearRightWheelCollider.enabled = false;
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
        if (wheelCollider.enabled)
        {
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
    }
}

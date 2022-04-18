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

    private void FixedUpdate()
    {
        UpdateWheels();
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

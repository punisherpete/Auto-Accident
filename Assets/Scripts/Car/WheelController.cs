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
    [SerializeField] private WheelCollider _rearReinforcedWheelSettings;
    [SerializeField] private WheelCollider _frontReinforcedWheelSettings;
    [Header("Sliding wheels")]
    [SerializeField] private WheelCollider _rearSlidingWheelSettings;
    [SerializeField] private WheelCollider _frontSlidingWheelSettings;

    private WheelFrictionCurve _rearDefaultWheelSidewaysFriction;
    private WheelFrictionCurve _frontDefaultWheelSidewaysFriction;
    private WheelFrictionCurve _rearDefaultWheelForwardFriction;
    private WheelFrictionCurve _frontDefaultWheelForwardFriction;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _frontDefaultWheelSidewaysFriction = _frontLeftWheelCollider.sidewaysFriction;
        _frontDefaultWheelForwardFriction = _frontLeftWheelCollider.forwardFriction;
        _rearDefaultWheelSidewaysFriction = _rearLeftWheelCollider.sidewaysFriction;
        _rearDefaultWheelForwardFriction = _rearLeftWheelCollider.forwardFriction;
    }

    private void FixedUpdate()
    {
        UpdateWheels();
        _isGrounded = _frontLeftWheelCollider.isGrounded || _frontRightWheelCollider.isGrounded;
    }

    public void ReinfoceWheelFrictionCurve()
    {
        _frontLeftWheelCollider.forwardFriction = _frontReinforcedWheelSettings.forwardFriction;
        _frontLeftWheelCollider.sidewaysFriction = _frontReinforcedWheelSettings.sidewaysFriction;
        _frontRightWheelCollider.forwardFriction = _frontReinforcedWheelSettings.forwardFriction;
        _frontRightWheelCollider.sidewaysFriction = _frontReinforcedWheelSettings.sidewaysFriction;
        _rearLeftWheelCollider.forwardFriction = _rearReinforcedWheelSettings.forwardFriction;
        _rearLeftWheelCollider.sidewaysFriction = _rearReinforcedWheelSettings.sidewaysFriction;
        _rearRightWheelCollider.forwardFriction = _rearReinforcedWheelSettings.forwardFriction;
        _rearRightWheelCollider.sidewaysFriction = _rearReinforcedWheelSettings.sidewaysFriction;
    }

    public void SetSlidingWheelFrictionCurve()
    {
        _frontLeftWheelCollider.forwardFriction = _frontSlidingWheelSettings.forwardFriction;
        _frontLeftWheelCollider.sidewaysFriction = _frontSlidingWheelSettings.sidewaysFriction;
        _frontRightWheelCollider.forwardFriction = _frontSlidingWheelSettings.forwardFriction;
        _frontRightWheelCollider.sidewaysFriction = _frontSlidingWheelSettings.sidewaysFriction;
        _rearLeftWheelCollider.forwardFriction = _rearSlidingWheelSettings.forwardFriction;
        _rearLeftWheelCollider.sidewaysFriction = _rearSlidingWheelSettings.sidewaysFriction;
        _rearRightWheelCollider.forwardFriction = _rearSlidingWheelSettings.forwardFriction;
        _rearRightWheelCollider.sidewaysFriction = _rearSlidingWheelSettings.sidewaysFriction;
    }

    public void ResetWheelFrictionCurve()
    {
        _frontLeftWheelCollider.forwardFriction = _frontDefaultWheelForwardFriction;
        _frontLeftWheelCollider.sidewaysFriction = _frontDefaultWheelSidewaysFriction;
        _frontRightWheelCollider.forwardFriction = _frontDefaultWheelForwardFriction;
        _frontRightWheelCollider.sidewaysFriction = _frontDefaultWheelSidewaysFriction;
        _rearLeftWheelCollider.forwardFriction = _rearDefaultWheelForwardFriction;
        _rearLeftWheelCollider.sidewaysFriction = _rearDefaultWheelSidewaysFriction;
        _rearRightWheelCollider.forwardFriction = _rearDefaultWheelForwardFriction;
        _rearRightWheelCollider.sidewaysFriction = _rearDefaultWheelSidewaysFriction;
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

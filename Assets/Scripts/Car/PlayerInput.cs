using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Car))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _brakingDragForce = 0.03f;
    [SerializeField] private float _maxMagnitudeToTurn = 10f;
    [SerializeField] private float _maxFrameJoysticVelocity = 0.5f;

    private SpeedLimit _speedLimit;
    private Mover _mover;
    private Coroutine _determineVelosity;
    private bool _isAbleToTurnInstanly = true;

    public event Action<float> CriticalReached;
    public float FrameJoysticVelocity { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _speedLimit = GetComponent<SpeedLimit>();
    }

    private void Update()
    {
        if (_joystick.IsPointerDown && _mover.IsAllWheelsOnGround)
        {
            if (_isAbleToTurnInstanly)
            {
                if (_joystick.Magnitude > _maxMagnitudeToTurn && Mathf.Abs(FrameJoysticVelocity) > _maxFrameJoysticVelocity)
                {
                    CriticalReached?.Invoke(_joystick.Horizontal);
                    StartCoroutine(ResettingElapsedTime());
                }
            }
        }
        if (_joystick.IsPointerDown)
        {
            _mover.TryChangeHorizontalOffset(_joystick.Horizontal);
            _speedLimit.SetRegularDragForce(0);
        }
        else if (!_mover.IsOnGround)
            _speedLimit.SetRegularDragForce(0);
        else
            _speedLimit.SetRegularDragForce(_brakingDragForce);
    }

    private void FixedUpdate()
    {
        if (_determineVelosity != null)
            StopCoroutine(_determineVelosity);
        _determineVelosity = StartCoroutine(DetermineVelocity(_joystick.Horizontal));
        if (_joystick.IsPointerDown && _mover.IsOnGround == false)
        {
            _mover.Rotate(_joystick.Horizontal);
            _mover.Drag(_joystick.Horizontal);
        }
    }

    private IEnumerator ResettingElapsedTime()
    {
        while (_joystick.Magnitude > _maxMagnitudeToTurn)
        {
            _isAbleToTurnInstanly = false; ;

            yield return null;
        }
        _isAbleToTurnInstanly = true;
    }

    private IEnumerator DetermineVelocity(float oldJoystickHorizontalInput)
    {
        yield return new WaitForEndOfFrame();
        FrameJoysticVelocity = oldJoystickHorizontalInput - _joystick.Horizontal;
    }
}

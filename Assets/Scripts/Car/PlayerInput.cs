using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Car))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _brakingDragForce = 0.03f;
    [SerializeField] private float _maxMagnitudeToTurn = 10f;

    private SpeedLimit _speedLimit;
    private Mover _mover;

    private bool _isAbleToTurnInstanly = true;

    public event Action<float> CriticalReached;

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
                if (_joystick.Magnitude > _maxMagnitudeToTurn)
                {
                    CriticalReached?.Invoke(_joystick.Horizontal);
                    StartCoroutine(ResettingElapsedTime());
                }
            }
        }
        if(_joystick.IsPointerDown && _mover.IsOnGround == false)
        {
            _mover.TurnOnTargetPoint();
        }
    }

    private void FixedUpdate()
    {
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

    private IEnumerator ResettingElapsedTime()
    {
        while (_joystick.Magnitude > _maxMagnitudeToTurn)
        {
            _isAbleToTurnInstanly = false; ;

            yield return null;
        }
        _isAbleToTurnInstanly = true;
    }
}

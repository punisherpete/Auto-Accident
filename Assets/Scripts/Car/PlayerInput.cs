using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Car))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _brakingDragForce = 0.03f;

    private SpeedLimit _speedLimit;
    private Mover _mover;

    private bool _elapsedTime = true;

    public event Action<float> CriticalReached;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _speedLimit = GetComponent<SpeedLimit>();
    }

    private void Update()
    {
        if (_joystick.IsPointerDown)
        {
            if (_elapsedTime)
            {
                if (Mathf.Abs(_joystick.Horizontal) >= 1)
                {
                    CriticalReached?.Invoke(_joystick.Horizontal);
                    StartCoroutine(ResettingElapsedTime());
                }
            }
        }
        print(_mover.GetCurrentSpeed() / 100);
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
        while (Mathf.Abs(_joystick.Horizontal) == 1)
        {
            _elapsedTime = false; ;

            yield return null;
        }
        _elapsedTime = true;
    }
}

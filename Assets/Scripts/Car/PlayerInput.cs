using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Car))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float brakingDragForce = 0.03f;

    private SpeedLimit _speedLimit;
    private Mover _mover;

    private float _start = 0f;
    private float _delta = 0f;
    private bool _elapsedTime = true;

    public event Action<float> CriticalReached;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _speedLimit = GetComponent<SpeedLimit>();
    }

    private void OnEnable()
    {
        _joystick.PointerDown += OnSetStart;
    }

    private void OnSetStart()
    {
        _start = _joystick.Horizontal;
    }

    private void FixedUpdate()
    {
        if (_joystick.IsPointerDown)
        {
            _mover.ChangeHorizontalOffset(_joystick.Horizontal);
            _speedLimit.SetRegularDragForce(0);
        }
        else
            _speedLimit.SetRegularDragForce(brakingDragForce);

    }

    private void Update()
    {
        if (_joystick.IsPointerDown)
        {
            if (_elapsedTime)
            {
                if(Mathf.Abs(_joystick.Horizontal) >= 1)
                {
                    CriticalReached?.Invoke(_joystick.Horizontal);
                    StartCoroutine(ResettingElapsedTime());
                }
            }
        }
    }

    private void OnDisable()
    {
        _joystick.PointerDown -= OnSetStart;
    }

    private IEnumerator ResettingElapsedTime()
    {
        while (Mathf.Abs(_joystick.Horizontal) == 1)
        {
            _elapsedTime = false; ;
            
            yield return null;
        }
        _elapsedTime = true; ;

    }

}

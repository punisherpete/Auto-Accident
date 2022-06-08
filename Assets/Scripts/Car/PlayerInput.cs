using UnityEngine;

[RequireComponent(typeof (Car))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float brakingDragForce = 0.03f;

    private SpeedLimit _speedLimit;
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _speedLimit = GetComponent<SpeedLimit>();
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

}

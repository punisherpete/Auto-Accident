using UnityEngine;

[RequireComponent(typeof (Car))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;

    private Car _car;

    private void Awake()
    {
        _car = GetComponent<Car>();
    }

    private void FixedUpdate()
    {
        _car.Input(_joystick.Horizontal);
    }

}

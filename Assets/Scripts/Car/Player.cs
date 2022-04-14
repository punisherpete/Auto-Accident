using UnityEngine;

[RequireComponent(typeof (Mover))]
public class Player : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        _mover.ChangeHorizontalOffset(_joystick.Horizontal);
    }

}

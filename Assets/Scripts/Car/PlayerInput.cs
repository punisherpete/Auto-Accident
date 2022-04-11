using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Mover))]
public class PlayerInput : MonoBehaviour
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

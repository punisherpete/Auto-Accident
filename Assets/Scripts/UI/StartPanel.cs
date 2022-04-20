using System;
using UnityEngine;

[RequireComponent(typeof(ControlAreaObserver))]
public class StartPanel : MonoBehaviour
{
    [SerializeField] private VariableJoystick _variableJoystick;

    private ControlAreaObserver _observer;

    private void Awake()
    {
        _observer = GetComponent<ControlAreaObserver>();
    }

    private void OnEnable()
    {
        _variableJoystick.PointerDown += OnPointerDown;
    }

    private void OnDisable()
    {
        _variableJoystick.PointerDown -= OnPointerDown;
    }

    private void OnPointerDown()
    {
        _observer.ActivateAfterDowned?.Invoke();
        gameObject.SetActive(false);
    }
}

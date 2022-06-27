using UnityEngine;
using UnityEngine.Events;

public class StartPanel : MonoBehaviour
{
    public UnityEvent ActivateAfterDown;

    [SerializeField] private VariableJoystick _variableJoystick;

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
        ActivateAfterDown?.Invoke();
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class StartPanel : MonoBehaviour
{
    public UnityEvent ActivateAfterDown;

    [SerializeField] private VariableJoystick _variableJoystick;
    [SerializeField] private MainMenu _mainMenu;

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
        _mainMenu.GameStarted();
        gameObject.SetActive(false);
    }
}

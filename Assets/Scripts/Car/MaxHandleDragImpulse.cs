using UnityEngine;

public class MaxHandleDragImpulse : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _intencity;

    private void OnEnable()
    {
        _playerInput.CriticalReached += OnInpulseRotate;
    }

    private void OnDisable()
    {
        _playerInput.CriticalReached -= OnInpulseRotate;
    }

    private void OnInpulseRotate(float direction)
    {
        _carRigidbody.AddTorque(new Vector3(0f, _intencity * direction, 0f), ForceMode.Impulse);
    }
}

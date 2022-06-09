using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class MaxHandleDragImpulse : MonoBehaviour
{
    [SerializeField] private float _intencity = 10000;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

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
        _rigidbody.AddTorque(new Vector3(0f, _intencity * direction, 0f), ForceMode.Impulse);
    }
}

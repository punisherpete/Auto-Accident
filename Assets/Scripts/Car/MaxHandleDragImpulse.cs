using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Transmission))]
public class MaxHandleDragImpulse : MonoBehaviour
{
    [SerializeField] private float _intencity = 10000;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Transmission _transferIndex;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _transferIndex = GetComponent<Transmission>();
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
        int changer = _transferIndex.GetCuttentTransferIndex();
        float clampedIntencity = _intencity / changer;
        _rigidbody.AddTorque(new Vector3(0f, clampedIntencity * direction, 0f), ForceMode.Impulse);
    }
}

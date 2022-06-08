using UnityEngine;

public class MaxHandleDragImpulse : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _intencity;
    [SerializeField] private float _speedDependencyProgression;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
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
        _intencity /= _mover.GetCurrentSpeed() / _speedDependencyProgression;
        _carRigidbody.AddTorque(new Vector3(0f, _intencity * direction, 0f), ForceMode.VelocityChange);
    }
}

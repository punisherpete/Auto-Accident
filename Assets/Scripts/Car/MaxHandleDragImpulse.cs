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
    private float _impulseRotateTime = 0.5f;
    private float _currentImpulseRotateTime;
    private void Update()
    {
        if(_currentImpulseRotateTime>=0)
            _currentImpulseRotateTime -= Time.deltaTime;
    }

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

    public void OnInpulseRotate(float direction)
    {
        if (_currentImpulseRotateTime >= 0)
            return;
        else
            _currentImpulseRotateTime = _impulseRotateTime;
        int transfer = _transferIndex.GetCuttentTransferIndex();
        float clampedIntencity = _intencity / (transfer + 1);
        if (transfer <= 1)
            clampedIntencity = _intencity;
        else if (transfer <= 3)
            clampedIntencity = _intencity / 2;
        else
            clampedIntencity = _intencity / 4;
        _rigidbody.AddTorque(new Vector3(0f, clampedIntencity * direction, 0f), ForceMode.Impulse);
        Debug.Log("Force " + _transferIndex.GetCuttentTransferIndex());
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpeedLimit : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float _maxDragModifier;
    [SerializeField] [Range(0,1)] private float _currentDragModifier = 0;
    [SerializeField] [Range(0,1)] private float _regularDragForce;
    [SerializeField] private float _dragForceChangeSpeed = 1;

    private Rigidbody _rigidBody;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_currentDragModifier > _regularDragForce) 
            _rigidBody.drag = _rigidBody.velocity.magnitude * _currentDragModifier;
        else
            _rigidBody.drag = _rigidBody.velocity.magnitude * _regularDragForce;
    }

    public void LimitedSpeed(float maxSpeed)
    {
        if (_rigidBody.velocity.magnitude > maxSpeed)
            SetModifier(Mathf.Lerp(_currentDragModifier, _maxDragModifier, _dragForceChangeSpeed * Time.fixedDeltaTime));
        else
            ResetModifier();
    }

    private void SetModifier(float value)
    {
        _currentDragModifier = Mathf.Clamp(value, 0, _maxDragModifier);
    }

    private void ResetModifier()
    {
        _currentDragModifier = 0;
    }
}

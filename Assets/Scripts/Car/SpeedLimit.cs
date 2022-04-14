using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpeedLimit : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float _dragModifier;
    
    private Rigidbody _rigidBody;
    private float _currentModifier;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _currentModifier = _dragModifier;
    }

    private void FixedUpdate()
    {
        _rigidBody.drag = _rigidBody.velocity.magnitude * _currentModifier;
    }

    public void SetModifier(float value)
    {
        _currentModifier = Mathf.Clamp(value, 0, 1);
    }

    public void ResetModifier()
    {
        _currentModifier = _dragModifier;
    }
}

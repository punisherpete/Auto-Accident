using UnityEngine;

public class CameraPointFollover : MonoBehaviour
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private Vector3 _offset;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_anchor.position + _offset);
    }
}

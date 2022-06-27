using UnityEngine;

public class CameraPointFollover : MonoBehaviour
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.position = Vector3.Lerp(_rigidbody.position, _anchor.position, Time.deltaTime * _speed);
    }
}

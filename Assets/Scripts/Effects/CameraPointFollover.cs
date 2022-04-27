using UnityEngine;

public class CameraPointFollover : MonoBehaviour
{
    [SerializeField] private Transform _anchor;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_anchor.position);
    }
}

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
        //_rigidbody.MovePosition(_anchor.position);
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, (_anchor.position - transform.position) * 10f, Time.deltaTime * 30f);
    }
}

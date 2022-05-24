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
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, (_anchor.position - transform.position) * 40f, Time.deltaTime * 10f);
    }
}

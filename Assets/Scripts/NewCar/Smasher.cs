using UnityEngine;

public class Smasher : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.forward * _speed, ForceMode.VelocityChange);
        }
    }
}

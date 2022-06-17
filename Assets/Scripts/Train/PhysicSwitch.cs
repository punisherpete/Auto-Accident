using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicSwitch : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mover>())
        {
            _rigidbody.isKinematic = false;
        }
    }
}

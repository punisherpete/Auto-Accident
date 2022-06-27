using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicSwitch : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.GetComponent<DamageEffector>())
            {
                _rigidbody.isKinematic = false;
                gameObject.transform.parent = null;
            }
        }   
    }
}

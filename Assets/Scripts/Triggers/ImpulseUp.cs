using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseUp : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rigidbody))
        {
            if (rigidbody.GetComponent<Mover>())
            {
                rigidbody.AddForce(Vector3.up * _force, ForceMode.VelocityChange);
            }
        }
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GlassPart : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isCrashed = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Crash(float force)
    {
        if (_isCrashed)
            return;

        _isCrashed = true;
        transform.SetParent(null);
        _rigidbody.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }
}

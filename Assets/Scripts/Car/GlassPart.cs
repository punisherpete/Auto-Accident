using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GlassPart : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isCrashed = false;

    public void Crash(float force)
    {
        if (_isCrashed)
            return;

        _isCrashed = true;
        _rigidbody.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }
}

using UnityEngine;

public class GlassPart : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isCrashed = false;

    private void Start()
    {
        _rigidbody.isKinematic = true;
    }

    public void Crash(float force)
    {
        if (_isCrashed)
            return;

        _isCrashed = true;
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * force, ForceMode.VelocityChange);
        Invoke(nameof(DeactivateWithDelay), 3f);
    }

    private void DeactivateWithDelay()
    {
        gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;

public class InteractionProcessor : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 1f;
    [SerializeField] private GameObject _parentRoot;

    private Transform _transform;

    public event Action<InteractionProcessor, Vector3> Affected;

    public float Sensitivity => _sensitivity;

    private void Start()
    {
        _transform = transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent)
        {
            if (collision.gameObject.transform.parent.transform.parent == _transform.parent.transform.parent)
                return;
        }

        Affected?.Invoke(this, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_parentRoot)
        {
            if (_parentRoot.GetComponent<PlayerInput>())
            {
                if (other.TryGetComponent(out GamePoint gamePoint))
                {
                    gamePoint.Collect();
                }
            }
        }
    }
}

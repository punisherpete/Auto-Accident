using System;
using UnityEngine;

public class InteractionProcessor : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 1f;

    private Transform _transform;

    public event Action<InteractionProcessor> Affected;

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

        Affected?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
            if (other.TryGetComponent(out GamePoint gamePoint))
                gamePoint.Collect();
        
    }
}

using System;
using UnityEngine;

public class InteractionProcessor : MonoBehaviour
{
    private Transform _transform;
    private float _elapsedTime;
    private float _takeDamageInterval = 1f;

    public event Action<InteractionProcessor, Vector3> Affected;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent)
        {
            if (collision.gameObject.transform.parent.transform.parent == _transform.parent.transform.parent)
                return;
        }

        if (_elapsedTime >= _takeDamageInterval)
        {
            _elapsedTime = 0f;
            Affected?.Invoke(this, _transform.position);
        }
    }
}

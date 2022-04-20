using System;
using UnityEngine;

public class InteractionProcessor : MonoBehaviour
{
    private Transform _transform;

    public event Action<InteractionProcessor> Affected;

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
}

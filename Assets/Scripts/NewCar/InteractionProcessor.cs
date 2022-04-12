using System;
using UnityEngine;

public class InteractionProcessor : MonoBehaviour
{
    [SerializeField] private bool _isSavingLocalPosition;

    private Transform _transform;
    private Vector3 _startLocalPosition;
    private Quaternion _startLocalRotation;

    private float _elapsedTime;
    private float _takeDamageInterval = 1f;

    public event Action<InteractionProcessor, Vector3> Affected;

    private void Start()
    {
        _transform = transform;
        _startLocalPosition = transform.localPosition;
        _startLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_isSavingLocalPosition)
        {
            _transform.localPosition = _startLocalPosition;
            _transform.localRotation = _startLocalRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.GetComponent<DamageEffector>())
            {
                if (_elapsedTime >= _takeDamageInterval)
                {
                    _elapsedTime = 0f;
                    Vector3 firstTouchPoint = collision.GetContact(0).point;
                    Affected?.Invoke(this, firstTouchPoint);

                }
            }
        }
    }
}

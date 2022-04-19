using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointsGlue : MonoBehaviour
{
    [SerializeField] private InteractionProcessor[] _interactionProcessors;
    [SerializeField] private float _hitAllowanceDelay = 3f;

    private Vector3 _startLocalPosition;
    private Quaternion _startLocalRotation;

    private float _timer;

    private void Start()
    {
        _startLocalPosition = transform.localPosition;
        _startLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer <= _hitAllowanceDelay)
        {
            _timer = 0;
            transform.localPosition = _startLocalPosition;
            transform.localRotation = _startLocalRotation;
        }
    }
}

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

    private void OnEnable()
    {
        //for (int i = 0; i < _interactionProcessors.Length; i++)
        //{
        //    _interactionProcessors[i].Affected += OnEnableJoints;
        //}
    }

    private void Start()
    {
        _startLocalPosition = transform.localPosition;
        _startLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (_timer >= _hitAllowanceDelay)
        {
            transform.localPosition = _startLocalPosition;
            transform.localRotation = _startLocalRotation;
        }
    }

    private void OnDisable()
    {
        //for (int i = 0; i < _interactionProcessors.Length; i++)
        //{
        //    _interactionProcessors[i].Affected -= OnEnableJoints;
        //}
    }

    private void OnEnableJoints(InteractionProcessor processor, Vector3 postition)
    {
        Destroy(this, _hitAllowanceDelay);
    }
}

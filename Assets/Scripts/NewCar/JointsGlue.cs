using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointsGlue : MonoBehaviour
{
    [SerializeField] private InteractionProcessor[] _interactionProcessors;
    [SerializeField] private float _hitBakeDelay = 1f;

    private Vector3 _startLocalPosition;
    private Quaternion _startLocalRotation;

    private void OnEnable()
    {
        for (int i = 0; i < _interactionProcessors.Length; i++)
        {
            _interactionProcessors[i].Affected += OnEnableJoints;
        }
    }

    private void Start()
    {
        _startLocalPosition = transform.localPosition;
        _startLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        transform.localPosition = _startLocalPosition;
        transform.localRotation = _startLocalRotation;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _interactionProcessors.Length; i++)
        {
            _interactionProcessors[i].Affected -= OnEnableJoints;
        }
    }

    private void OnEnableJoints(InteractionProcessor processor, Vector3 postition)
    {
        Destroy(this, _hitBakeDelay);
    }
}

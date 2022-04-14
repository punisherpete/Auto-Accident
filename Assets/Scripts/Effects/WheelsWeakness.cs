using System.Collections.Generic;
using UnityEngine;

public class WheelsWeakness : MonoBehaviour
{
    [SerializeField] private List<WheelCollider> _wheelColliders;
    [SerializeField] private List<WheelController> _wheelRotators;
    [SerializeField] private List<Collider> _colliders;
    [SerializeField] private InteractionProcessor[] _interactionProcessors;

    private void OnEnable()
    {
        for (int i = 0; i < _interactionProcessors.Length; i++)
        {
            _interactionProcessors[i].Affected += TearRandomWheel;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _interactionProcessors.Length; i++)
        {
            _interactionProcessors[i].Affected -= TearRandomWheel;
        }
    }

    private void TearRandomWheel(InteractionProcessor processor, Vector3 postition)
    {
        if (_wheelColliders.Count <= 2)
            return;

        int rand = Random.Range(0, _wheelColliders.Count - 1);
        for (int i = 0; i < _wheelColliders.Count; i++)
        {
            if (i == rand) 
            {
                _wheelColliders[i].enabled = false;
                _wheelRotators[i].enabled = false;
                _colliders[i].enabled = true;
                _colliders[i].gameObject.AddComponent<Rigidbody>();
                _colliders[i].transform.parent = null;
                _wheelColliders.Remove(_wheelColliders[i]);
                _wheelRotators.Remove(_wheelRotators[i]);
                _colliders.Remove(_colliders[i]);
            }
        }
    }
}

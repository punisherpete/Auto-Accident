using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolition : MonoBehaviour
{
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Transform _burstDirection;
    [SerializeField] private float _burstForce;
    [SerializeField] private ParticleSystem[] _burstEffects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>())
        {
            Burst();
        }
    }

    [ContextMenu("Burst")]
    private void Burst()
    {
        for (int i = 0; i < _colliders.Length; i++)
        {
            Rigidbody partRigidbody = _colliders[i].GetComponent<Rigidbody>();
            partRigidbody.useGravity = true;
            partRigidbody.AddForce(_burstDirection.forward * _burstForce, ForceMode.VelocityChange);
            partRigidbody.AddTorque(_burstDirection.right * _burstForce, ForceMode.VelocityChange);
            _colliders[i].transform.parent = null;
            _colliders[i].gameObject.layer = LayerMask.GetMask("Default");
        }

        PlayEffects();
    }

    private void PlayEffects()
    {
        for (int i = 0; i < _burstEffects.Length; i++)
        {
            _burstEffects[i].Play();
        }
    }
}

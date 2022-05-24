using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private bool _isDisableAfterTriggerEnter;
    [Header("Options")]
    [SerializeField] private float _boostAccelation = 3f;
    [SerializeField] private float _boostSpeed = 1.5f;
    [SerializeField] private float _boostTime = 4f;
    [SerializeField] private float _boostImpulseForce = 1500f;
    [Header("Effects")]
    [SerializeField] private ParticleSystem _tookExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Mover mover))
        {
            mover.SetBoost(_boostAccelation, _boostSpeed, _boostImpulseForce, _boostTime);
            _tookExplosion.transform.parent = null;
            _tookExplosion.Play();
            if (_isDisableAfterTriggerEnter)
                gameObject.SetActive(false);
        }
    }
}

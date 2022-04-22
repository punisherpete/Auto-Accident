using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private bool _isDisableAfterTriggerEnter;
    [Header("Options")]
    [SerializeField] private float _boostAccelation = 3f;
    [SerializeField] private float _boostSpeeed = 1.5f;
    [SerializeField] private float _boostTime = 4f;
    [SerializeField] private float _boostImpulseForce = 1500f;
    [Header("Effects")]
    [SerializeField] private ParticleSystem _tookExplosion;
    [SerializeField] private ParticleSystem _speedWindLines;



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Mover mover))
        {
            mover.SetBoost(_boostAccelation, _boostSpeeed, _boostImpulseForce, _boostTime);
            ParticleSystem newEffect = Instantiate(_tookExplosion, transform.position, transform.rotation, null);
            //Instantiate(_speedWindLines, mover.gameObject.transform.position, Quaternion.identity, mover.transform);
            newEffect.transform.localScale = transform.localScale;
            if (_isDisableAfterTriggerEnter)
                gameObject.SetActive(false);
        }
    }
}

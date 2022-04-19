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


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Mover mover))
        {
            mover.SetBoost(_boostAccelation, _boostSpeeed, _boostImpulseForce, _boostTime);
            if (_isDisableAfterTriggerEnter)
                gameObject.SetActive(false);
        }
    }
}

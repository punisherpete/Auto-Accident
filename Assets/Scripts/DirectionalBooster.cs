using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBooster : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float _boostAccelation = 3f;
    [SerializeField] private float _boostSpeed = 1.5f;
    [SerializeField] private float _boostTime = 2f;
    [SerializeField] private float _boostImpulseForce = 1500f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Mover mover))
            mover.SetBoost(_boostAccelation,_boostSpeed,_boostImpulseForce,_boostTime);
    }
}

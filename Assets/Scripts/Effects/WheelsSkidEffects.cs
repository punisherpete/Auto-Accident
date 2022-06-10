using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Mover))]
public class WheelsSkidEffects : MonoBehaviour
{
    [Header("EFFECTS")]
    [Space(10)]
    [SerializeField] private bool _useEffects = false;

    [SerializeField] private ParticleSystem _rLWParticleSystem;
    [SerializeField] private ParticleSystem _rRWParticleSystem;

    [Space(10)]
    [SerializeField] private TrailRenderer _rLWTireSkid;
    [SerializeField] private TrailRenderer _rRWTireSkid;

    [Space(10)]
    [SerializeField] private float _immintHeightRange;


    private Rigidbody _carRigidbody;
    private float _localVelocityX;
    private bool _isDrifting;
    private bool _isFlying;
    private float _currentGroundSurfacePoint;
    private Mover _mover;

    private void Start()
    { 
        _carRigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _localVelocityX = transform.InverseTransformDirection(_carRigidbody.velocity).x;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            _currentGroundSurfacePoint = hit.point.y;
        }

        _isFlying = transform.position.y - _currentGroundSurfacePoint >= _immintHeightRange;

        TryDrift();
    }

    private void TryDrift()
    {
        if (Mathf.Abs(_localVelocityX) > 2.5f && !_isFlying && _mover.IsAllWheelsOnGround)
        {
            _isDrifting = true;
            DriftCarPS();
        }
        else
        {
            _isDrifting = false;
            DriftCarPS();
        }
    }

    public void DriftCarPS()
    {
        if (_useEffects)
        {
            try
            {
                if (_isDrifting)
                {
                    _rLWParticleSystem.Play();
                    _rRWParticleSystem.Play();
                    _rLWTireSkid.emitting = true;
                    _rRWTireSkid.emitting = true;
                }
                else if (!_isDrifting)
                {
                    _rLWParticleSystem.Stop();
                    _rRWParticleSystem.Stop();
                    _rLWTireSkid.emitting = false;
                    _rRWTireSkid.emitting = false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        else
        {
            if (_rLWParticleSystem)
            {
                _rLWParticleSystem.Stop();
            }
            if (_rRWParticleSystem)
            {
                _rRWParticleSystem.Stop();
            }
            if (_rLWTireSkid)
            {
                _rLWTireSkid.emitting = false;
            }
            if (_rRWTireSkid)
            {
                _rRWTireSkid.emitting = false;
            }
        }
    }
}

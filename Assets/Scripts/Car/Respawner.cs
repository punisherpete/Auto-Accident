using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WheelsSkidEffects))]
public class Respawner : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _respawnTime = 2f;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private List<GameObject> _objectsToReplaceLayer;
    [SerializeField] private float _safeDistanceForSafeMode = 5f;
    [SerializeField] private float _criticalHorizontalOffset = 10f;

    [SerializeField] private int _safeLayerIndex = 8;
    [SerializeField] private bool _isRespawnAllowed = true;
    [SerializeField] private string _respawnAnimationName;

    private SplineProjectorObserver _splineProjectorObserver = null;
    private int[] _baseLayerIndex;
    private bool _isSafeModeActivated = false;
    private int _respawnHash;
    private int _idleHasn;

    private float _respawnTimer;
    private Mover _mover;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private WheelsSkidEffects _wheelsSkidEffects;

    public event Action Proceed;

    private void Awake()
    {
        _respawnHash = Animator.StringToHash(_respawnAnimationName);
        _idleHasn = Animator.StringToHash("Idle");
        _mover = GetComponent<Mover>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _wheelsSkidEffects = GetComponent<WheelsSkidEffects>();
        if(gameObject.TryGetComponent(out SplineProjectorObserver projectorObserver))
            _splineProjectorObserver = projectorObserver;
        _baseLayerIndex = new int[_objectsToReplaceLayer.Count];
        for (int i = 0; i < _objectsToReplaceLayer.Count; i++)
        {
            _baseLayerIndex[i] = _objectsToReplaceLayer[i].layer;
        }
    }

    private void Update()
    {
        if (_isRespawnAllowed == false)
            return;
        if (_rigidbody.velocity.magnitude < 2f)
            _respawnTimer += Time.deltaTime;
        else if(_splineProjectorObserver != null && _splineProjectorObserver.IsGoesBeyondCriticalDistance(_criticalHorizontalOffset))
            _respawnTimer += Time.deltaTime;
        else if (_splineProjectorObserver != null && Mathf.Abs(Quaternion.Angle(_rigidbody.rotation, _splineProjectorObserver.Projector.transform.rotation)) > 90)
            _respawnTimer += Time.deltaTime;
        else
            _respawnTimer = 0;
        if (_isSafeModeActivated && _carsObserver.IsInSafeZone(transform, _safeDistanceForSafeMode))
            StartCoroutine(DeactivateSafeMode(2f));
        if (_respawnTimer >= _respawnTime)
            RespawnCar();
    }

    public void ProhibitRespawn()
    {
        _isRespawnAllowed = false;
    }

    public void AllowRespawn()
    {
        _isRespawnAllowed = true;
    }

    public void SetCriticalHorizontalOffset(float criticalOffset)
    {
        _criticalHorizontalOffset = criticalOffset;
    }

    public void SetRespawnPoint(Transform point)
    {
        _respawnPoint = point;
    }

    private void RespawnCar()
    {
        _wheelsSkidEffects.SetOnEffects(false);
        StopCoroutine(RotateCarInNextFrame());
        _mover.ProhibitRotationForAWhile(2f);
        _respawnTimer = 0;
        _rigidbody.isKinematic = true;
        transform.position = _respawnPoint.position;
        _splineProjectorObserver.Projector.Rebuild();
        if (_splineProjectorObserver != null)
            StartCoroutine(RotateCarInNextFrame());
        else
            transform.rotation = _respawnPoint.rotation;
        ActivateSafeMode();
        _rigidbody.isKinematic = false;
        _mover.AllowChangeOffset();
        _mover.StartMoving();
        Proceed?.Invoke();
    }

    private void ActivateSafeMode()
    {
        _animator.Play(_respawnHash);
        _isSafeModeActivated = true;
        foreach (var item in _objectsToReplaceLayer)
        {
            item.layer = _safeLayerIndex;
        }
    }

    private IEnumerator RotateCarInNextFrame()
    {
        yield return new WaitForFixedUpdate();
        _rigidbody.isKinematic = true;
        transform.rotation = _splineProjectorObserver.Projector.transform.rotation;
        _rigidbody.isKinematic = false;
    }

    private IEnumerator DeactivateSafeMode(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _animator.Play(_idleHasn);
        _isSafeModeActivated = false;
        for (int i = 0; i < _objectsToReplaceLayer.Count; i++)
        {
            _objectsToReplaceLayer[i].layer = _baseLayerIndex[i];
        }
        _wheelsSkidEffects.SetOnEffects(false);
    }
}

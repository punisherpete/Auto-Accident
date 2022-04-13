using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Mover))]
public class CarRespawner : MonoBehaviour
{
    [SerializeField] private CarsObserver _carsObserver;
    [SerializeField] private float _respawnTime = 2f;
    [SerializeField] private float _respawnHeight = 2f;
    [SerializeField] private List<GameObject> _objectsToReplaceLayer;
    [SerializeField] private float _safeDistanceForSafeMode = 5f;
    [SerializeField] private int _safeLayerIndex = 8;

    private int _baseLayerIndex;
    private bool _isSafeModeActivated = false;
    private float _respawnTimer;
    private Mover _mover;
    private Rigidbody _rigidbody;
    

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rigidbody = GetComponent<Rigidbody>();
        _baseLayerIndex = gameObject.layer;
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude < 0.5f)
            _respawnTimer += Time.deltaTime;
        else if (Mathf.Abs(Quaternion.Angle(_rigidbody.rotation, _mover.CurrentNode.rotation))>90)
            _respawnTimer += Time.deltaTime / 2;
        else
            _respawnTimer = 0;
        if (_isSafeModeActivated && _carsObserver.IsCarInSafeZone(transform, _safeDistanceForSafeMode))
            DeactivateSafeMode();
    }

    private void FixedUpdate()
    {
        if (_respawnTimer >= _respawnTime)
            RespawnCar(); 
    }

    private void RespawnCar()
    {
        _respawnTimer = 0;
        _rigidbody.isKinematic = true;
        transform.position = new Vector3(_mover.CurrentNode.position.x, _mover.CurrentNode.position.y + _respawnHeight, _mover.CurrentNode.position.z);
        transform.rotation = _mover.CurrentNode.rotation;
        _rigidbody.isKinematic = false;
        _mover.StartMoving();
        ActivateSafeMode();
    }

    private void ActivateSafeMode()
    {
        _isSafeModeActivated = true;
        foreach (var item in _objectsToReplaceLayer)
        {
            item.layer = _safeLayerIndex;
        }
    }

    private void DeactivateSafeMode()
    {
        _isSafeModeActivated = false;
        foreach (var item in _objectsToReplaceLayer)
        {
            item.layer = _baseLayerIndex;
        }
    }
}

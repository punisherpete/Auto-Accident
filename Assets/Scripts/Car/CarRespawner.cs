using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Mover))]
public class CarRespawner : MonoBehaviour
{
    [SerializeField] private float _respawnTime = 2f;
    [SerializeField] private float _respawnHeight = 3f;

    [SerializeField] private float _timer;
    private Mover _mover;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Mathf.Abs(_mover.CurrentRotationWheel)>0.5f || _rigidbody.velocity.magnitude<1)
            _timer += Time.deltaTime;
        else
            _timer = 0;
        if(_timer>=_respawnTime)
        {
            _timer = 0;
            _rigidbody.isKinematic = true;
            transform.position = new Vector3(_mover.CurrentNode.position.x, _mover.CurrentNode.position.y + _respawnHeight, _mover.CurrentNode.position.z);
            transform.rotation = _mover.CurrentNode.rotation;
            _rigidbody.isKinematic = false;
        }
    }
}

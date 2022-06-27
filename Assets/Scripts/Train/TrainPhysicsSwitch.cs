using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Dreamteck.Splines.Examples;
using System;

public class TrainPhysicsSwitch : MonoBehaviour
{
    [SerializeField] private TrainEngine _trainEngine;
    [SerializeField] private Wagon[] _wagons;

    [SerializeField] private float _breakJointsDelay = 4f;
    [SerializeField] private float _force = 4f;

    [SerializeField] private Node[] _connectors;
    [SerializeField] private SplineComputer _splineComputer;
    [SerializeField] private Rigidbody[] _vagonsRigidbodies;
    [SerializeField] private Rigidbody[] _bodiesToKeepImpulse;

    private float _moveDuration;
    private SplineFollower _follower;
    private Rigidbody _rigidbody;
    private float Speed => _follower.followSpeed;

    public event Action Enabled;

    private void Awake()
    {
        _follower = GetComponent<SplineFollower>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    [ContextMenu("Become Physics")]
    public void BecomePhysics()
    {
        //_trainEngine.GetComponent<SplineFollower>().enabled = false;
        StartCoroutine(GoForward(_moveDuration));

        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        for (int i = 1; i < _wagons.Length; i++)
        {
            if (_wagons[i].TryGetComponent(out SplinePositioner splinePositioner))
                splinePositioner.enabled = false;
            //_connectors[i].enabled = true;
        }
        //_splineComputer.enabled = true;

        for (int i = 0; i < _vagonsRigidbodies.Length; i++)
        {

            _vagonsRigidbodies[i].interpolation = RigidbodyInterpolation.None;
            _vagonsRigidbodies[i].collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        StartCoroutine(BreakJoints(_moveDuration + _breakJointsDelay));
        Enabled?.Invoke();
    }

    private IEnumerator GoForward(float actionTime)
    {
        float time = 0;
        Vector3 newVelocity = transform.right * _force;
        yield return new WaitForSeconds(0);
        while (time < 1f)
        {
            _rigidbody.velocity = newVelocity;
            for (int i = 0; i < _bodiesToKeepImpulse.Length; i++)
            {
                _vagonsRigidbodies[i].isKinematic = false;
                _vagonsRigidbodies[i].transform.parent = null;
                _bodiesToKeepImpulse[i].AddForce(newVelocity, ForceMode.VelocityChange);
            }
            time += Time.deltaTime / actionTime;
            yield return null;
        }
    }

    private IEnumerator BreakJoints(float actionTime)
    {
        yield return new WaitForSeconds(actionTime);

        for (int i = 1; i < _vagonsRigidbodies.Length; i++)
        {
            if (_vagonsRigidbodies[i].TryGetComponent(out SpringJoint springJoint))
            {
                springJoint.breakForce = 0f;
                springJoint.breakTorque = 0f;
            }
        }
    }

    public void SetForceDuration(float value)
    {
        _moveDuration = value;
    }
}

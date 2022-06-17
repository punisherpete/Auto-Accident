using System.Collections;
using UnityEngine;
using Dreamteck.Splines;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float _upSpeed = 5f;
    [SerializeField] private float _downSpeed = 5f;
    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private float _accelerationDuration;
    [SerializeField] private float _deccelerationDuration;
    [SerializeField] private TrainPhysicsSwitch _trainPhysicsSwitch;

    private float _lastSpeed;

    [ContextMenu("Accelerate")]
    public void Accelerate()
    {
        _trainPhysicsSwitch.SetForceDuration(1f);
        _splineFollower.followSpeed = _lastSpeed;
        StartCoroutine(ChangeSpeed(_upSpeed, _accelerationDuration));
    }

    public void Deccelerate()
    {
        _trainPhysicsSwitch.SetForceDuration(0.2f);
        _splineFollower.followSpeed = _lastSpeed;
        StartCoroutine(ChangeSpeed(_downSpeed, _deccelerationDuration));
    }

    public IEnumerator ChangeSpeed(float newSpeed, float duration)
    {
        _lastSpeed = _splineFollower.followSpeed;
        float time = 0;
        while (time < 1)
        {
            _splineFollower.followSpeed = Mathf.Lerp(_splineFollower.followSpeed, newSpeed, time * time);
            time += Time.deltaTime / duration;
            yield return null;
        }
        _splineFollower.followSpeed = newSpeed;

        if (newSpeed == 0)
        {
            _splineFollower.enabled = false;
        }
    }
}

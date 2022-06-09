using Dreamteck.Splines;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private SplineProjector _splineProjector;
    [SerializeField] private SplineProjector _targetSplineProjector;
    [SerializeField] private TargetPointMover _targetPointMover;


    public Transform TargetPoint => _targetPointMover.transform;
    public Transform CurrentSplineProjector => _splineProjector.transform;

    public void Initialize(SplineComputer spline)
    {
        _splineProjector.spline = spline;
        _targetSplineProjector.spline = spline;
    }

    public void MovePath(float criticalOffset, float offsetSpeed, float input)
    {
        _targetPointMover.Move(criticalOffset, offsetSpeed, input);
    }
}

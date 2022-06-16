using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class PathController : MonoBehaviour
{
    [Header("Car Patch")]
    [SerializeField] private SplineComputer _spline;
    [Header("Generation")]
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private Transform _instancePlace;
    [Header("Path")]
    [SerializeField] private Path _path;

    private float _newTargetOffset;
    private float _offsetSpeed;
    private bool _isMovingToNewTargetPosition = false;

    public Transform TargetPoint => _path.TargetPoint;
    public Transform CurrentPoint => _path.CurrentSplineProjector;


    private void Start()
    {
        _path.Initialize(_spline);
    }

    private void LateUpdate()
    {
        if(_isMovingToNewTargetPosition)
        {
            _path.MovePathToNewTargetOffset(_offsetSpeed, _newTargetOffset);
            if (_path.IsPointAchiveToTargetOffset)
                _isMovingToNewTargetPosition = false;       
        }
    }

    public void ChangeHorizontalOffset(float criticalOffset, float offsetSpeed, float input)
    {
        _offsetSpeed = offsetSpeed;
        if(!_isMovingToNewTargetPosition)
            _path.MovePath(criticalOffset, offsetSpeed, input);
    }

    public void SetNewTargetOffset(float offsetSpeed, float targetOffset)
    {
        if (_isMovingToNewTargetPosition)
            return;
        else
        {
            _offsetSpeed = offsetSpeed;
            _newTargetOffset = targetOffset;
            _isMovingToNewTargetPosition = true;
        }
    }
}
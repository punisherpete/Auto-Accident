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

    public Transform TargetPoint => _path.TargetPoint;
    public Transform CurrentPoint => _path.CurrentSplineProjector;

    private void Start()
    {
        _path.Initialize(_spline);
    }

    public void ChangeHorizontalOffset(float criticalOffset, float offsetSpeed, float input)
    {
        _path.MovePath(criticalOffset, offsetSpeed, input);
    }
}
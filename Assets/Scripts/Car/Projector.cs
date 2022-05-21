using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    [SerializeField] private Car _car;

    private SplineProjector _splineProjector;

    private void Awake()
    {
        _splineProjector = GetComponent<SplineProjector>();
        _splineProjector.projectTarget = _car.transform;
    }

    public Car GetCar()
    {
        return _car;
    }
}

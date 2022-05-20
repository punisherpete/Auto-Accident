using UnityEngine;
using Dreamteck.Splines;

public class SplineProjectorObserver : MonoBehaviour
{
    [SerializeField] private SplineProjector _splineProjector;

    public bool IsGoesBeyondCriticalDistance(float criticalOffset)
    {
        return Vector3.Distance(transform.position, _splineProjector.transform.position) > criticalOffset;
    }

    public double GetCurrentPercent()
    {
        return _splineProjector.result.percent;
    }
}

using UnityEngine;

public class SplineProjectorObserver : MonoBehaviour
{
    [SerializeField] private Projector _projector;

    public bool IsGoesBeyondCriticalDistance(float criticalOffset)
    {
        return Vector3.Distance(transform.position, _projector.transform.position) > criticalOffset;
    }
}

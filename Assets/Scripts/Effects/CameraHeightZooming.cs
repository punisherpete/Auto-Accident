using Cinemachine;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineProjector))]
public class CameraHeightZooming : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private float _moveSpeed = 0.05f;
    [SerializeField] private float _allowOffsetWalue = 1;

    private float _currentGroundSurfacePoint;
    private float _previousGroundSurfacePoint;
    private SplineProjector _projector;

    private void Awake()
    {
        _projector = GetComponent<SplineProjector>();
        _previousGroundSurfacePoint = _currentGroundSurfacePoint;
    }

    private void LateUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            _currentGroundSurfacePoint = hit.point.y;

        if (_currentGroundSurfacePoint - _previousGroundSurfacePoint >= _allowOffsetWalue)
        {
            _projector.motion.offset = Vector2.Lerp(_projector.motion.offset,
                new Vector2(0, _car.transform.position.y - _currentGroundSurfacePoint), _moveSpeed * Time.deltaTime);
            _previousGroundSurfacePoint = _currentGroundSurfacePoint;
        }
    }
}

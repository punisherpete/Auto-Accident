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
    private SplineProjector _projector;

    private void Awake()
    {
        _projector = GetComponent<SplineProjector>();
    }

    private void Update()
    {
        if (Physics.Raycast(_car.transform.position, Vector3.down, out RaycastHit hit))
        {
            _currentGroundSurfacePoint = hit.point.y;
            Debug.DrawRay(_car.transform.position, Vector3.down * 100f, Color.red);
        }

        if (_car.transform.position.y - _currentGroundSurfacePoint >= _allowOffsetWalue)
        {
            _projector.motion.offset = Vector2.Lerp(_projector.motion.offset,
                new Vector2(0, _car.transform.position.y - _currentGroundSurfacePoint), _moveSpeed * Time.deltaTime);

        }
        else
        {
            _projector.motion.offset = Vector2.Lerp(_projector.motion.offset,
                new Vector2(0f, 0f), _moveSpeed * Time.deltaTime);
        }
    }
}

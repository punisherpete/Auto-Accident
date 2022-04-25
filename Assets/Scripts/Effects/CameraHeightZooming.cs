using Cinemachine;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineProjector))]
public class CameraHeightZooming : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private float _moveSpeed = 0.05f;

    private float _startCarY;
    private SplineProjector _projector;

    private void Awake()
    {
        _projector = GetComponent<SplineProjector>();
    }

    private void LateUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            _startCarY = hit.point.y;

        _projector.motion.offset = Vector2.Lerp(_projector.motion.offset, 
            new Vector2(0, _car.transform.position.y - _startCarY), _moveSpeed * Time.deltaTime);
    }
}

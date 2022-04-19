using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transmission))]
public class Mover : MonoBehaviour
{

    [SerializeField] private float _criticalOffset = 2f;
    [SerializeField] private float _offsetSpeed = 2f;
    [SerializeField] private float _maxSpeed = 30;
    [SerializeField] private float _motorForce = 100f;
    [SerializeField] private float _breakForce = 1000f;
    [SerializeField] private float _maxSteerAngle = 35f;
    [SerializeField] private float _turningPower = 20f;
    [SerializeField] private float _centerOfMass = -.5f;

    private PathMover _pathController = null;
    private float _currentSteerAngle;
    private float _currentBreakForce;
    private bool _isStop;
    private Transform _targetNode;
    private Transform _currentNode;
    private float _currentRotationWheel = 0;
    private float _horizontalOffset;
    private float _breakingTimer = 0f;
    private WheelController _wheelController;
    private Transmission _transmission;

    public float CurrentRotationWheel => _currentRotationWheel;
    public Transform CurrentNode => _currentNode;
    public bool IsStop => _isStop;

    private void Awake()
    {
        _wheelController = GetComponent<WheelController>();
        _transmission = GetComponent<Transmission>();
        GetComponent<Rigidbody>().centerOfMass = Vector3.up * _centerOfMass;
    }

    private void FixedUpdate()
    {
        if (_targetNode == null || _pathController == null)
            return;
        if (_breakingTimer > 0)
            _breakingTimer -= Time.fixedDeltaTime;
        HandleMotor();
        HandleSteering();
        HandleGroundedPositioning();
    }

    public void SetPathController(PathMover pathController)
    {
        _pathController = pathController;
    }

    public void SetTargetNode(Transform currentNode,Transform targetNode)
    {
        _currentNode = currentNode;
        _targetNode = targetNode;
    }

    public void ChangeHorizontalOffset(float horizontalInput)
    {
        if (_horizontalOffset > _criticalOffset)
            _horizontalOffset -= _offsetSpeed * Time.fixedDeltaTime;
        else if (_horizontalOffset < -_criticalOffset)
            _horizontalOffset += _offsetSpeed * Time.fixedDeltaTime;
        else
            _horizontalOffset += horizontalInput * _offsetSpeed * Time.fixedDeltaTime;
    }

    public void PauseMoving(float time)
    {
        Debug.LogWarning("Break");
        _breakingTimer = time;
    }

    public void StopMoving()
    {
        Debug.LogError("Stop");
        _isStop = true;
        _transmission.TurnOnNeutral();
    }

    public void StartMoving()
    {
        _isStop = false;
        _transmission.SetMaxSpeed(_maxSpeed);
        _transmission.TurnOnDrive();
    }

    public void SetMaxSpeed(float value)
    {
        _maxSpeed = value;
        _transmission.SetMaxSpeed(_maxSpeed);
    }

    private void HandleMotor()
    {
        _wheelController.SetForce(_transmission.GetAcceleration() * _motorForce);
        if(_isStop)
            _currentBreakForce = _breakForce;
        else
            _currentBreakForce = _breakingTimer > 0 ? _breakForce : 0f;
        _wheelController.ApplyBreaking(_currentBreakForce);
    }

    private void HandleSteering()
    {
        _pathController.MovePath(_horizontalOffset);
        Vector3 realitiveVector = transform.InverseTransformPoint(_targetNode.position);
        realitiveVector = realitiveVector / realitiveVector.magnitude;
        float rotationToTargetSample = realitiveVector.x / realitiveVector.magnitude;
        _currentRotationWheel = Mathf.Lerp(_currentRotationWheel, rotationToTargetSample, _turningPower * Time.fixedDeltaTime);
        _currentSteerAngle = _maxSteerAngle * _currentRotationWheel;
        _wheelController.SetSeetAngle(_currentSteerAngle);
    }

    private void HandleGroundedPositioning()
    {
        RaycastHit ground;
        if (Physics.Raycast(transform.position, Vector3.down, out ground))
        {
            if (Vector3.Distance(transform.position, ground.point) > 0.5f)
            {
                _wheelController.DisableWheelColliders();
            }
            else
            {
                _wheelController.EnableWheelColliders();
            }
        }
    }
}

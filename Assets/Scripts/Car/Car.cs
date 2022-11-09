using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
public class Car : MonoBehaviour
{
    public event UnityAction<Car> OnFinished;
    public event UnityAction Won;
    public event UnityAction Lost;

    [SerializeField] private CarType _type;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _aiName;

    [Header("Debug")]
    [SerializeField] private bool _showSpeed = false;

    private Mover _mover;
    private Respawner _respawner;
    private bool _isFinished = false;
    private SplineProjectorObserver _splineProjectorObserver;
    private string _name;

    public string Name => _name;
    public CarType Type => _type;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        TryGetComponent(out Respawner respawner);
        _respawner = respawner;
        _splineProjectorObserver = GetComponent<SplineProjectorObserver>();
    }

    private void Start()
    {
        StopMachine();
    }

    private void FixedUpdate()
    {
        if (_showSpeed)
            Debug.Log("Speed: " + _mover.GetCurrentSpeed() + " Max speed: " + _mover.GetMaxSpeed() + " Acceleration: " + _mover.GetAcceleration());

    }

    public void Win()
    {
        Won?.Invoke();
        FindObjectOfType<Ads>().ShowInterstitialAd();
    }

    public void Lose()
    {
        Lost?.Invoke();
    }

    public void ChangeOffsetSpeed(float speed)
    {
        _mover.SetOffsetSpeed(speed);
    }

    public void SetSpeedLimit(float speed)
    {
        _mover.SetMaxSpeed(speed);
    }

    public void SetMaxOffset(float value)
    {
        _mover.SetCriticalHorizontalOffset(value);
    }

    public void TrySetRespawnPoint(Transform point)
    {
        if(_respawner!=null)
            _respawner.SetRespawnPoint(point);
    }

    public void StrengthenWheels()
    {
        _mover.StrengthenWheels();
    }

    public void SetSlidingWheel(float slidingTime)
    {
        _mover.SetSlidingWheel(slidingTime);
    }

    public void TrySetCriticalRespawnOffset(float criticalOffset)
    {
        if(_respawner!=null)
            _respawner.SetCriticalHorizontalOffset(criticalOffset);
    }

    public bool TryResetToDefaultWheel()
    {
        return _mover.TryResetToDefaultWheel();
    }

    public void StopMachine()
    {
        _mover.StopMoving();
        if(_respawner != null)
            _respawner.ProhibitRespawn();
    }

    public void StartMachine()
    {
        _mover.StartMoving();
        if (_respawner != null)
            _respawner.AllowRespawn();
    }

    public void TurnControlOnRoad()
    {
        _mover.AllowChangeOffset();
    }

    public void TurnOffControlOnRoad()
    {
        _mover.ProhibitChangeOffset();
    }

    public void TrySetNewTargetOffset(float targetOffset)
    {
        _mover.TrySetNewTargetOffset(targetOffset);
    }
    
    public void SetNewTargetOffset(float targetOffset)
    {
        _mover.SetNewTargetOffset(targetOffset);
    }

    public void Finish()
    {
        if (_isFinished)
            return;
        _isFinished = true;
        OnFinished?.Invoke(this);
    }

    public void SetName(string name)
    {
        _name = name;
        if(_type == CarType.Player)
        {
            _playerName.gameObject.SetActive(true);
            _playerName.text = name;
        }
        else
        {
            _aiName.gameObject.SetActive(true);
            _aiName.text = name;
        }
    }

    public float GetCurrentSlidingTime()
    {
        return _mover.SlidingTime;
    }

    public double GetCurrentSplinePercent()
    {
        return _splineProjectorObserver.GetCurrentPercent();
    }

    public float GetCurrentSpeed()
    {
        return _mover.GetCurrentSpeed();
    }
}
public enum CarType
{
    Player,
    AI,
    Police
}
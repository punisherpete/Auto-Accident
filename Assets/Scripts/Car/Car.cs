using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Respawner))]
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

    private string _name;
    public string Name => _name;
    public CarType Type => _type;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _respawner = GetComponent<Respawner>();
    }

    private void Start()
    {
        StopMashine();
    }

    private void FixedUpdate()
    {
        if (_showSpeed)
            Debug.Log("Speed: " + _mover.GetCurrentSpeed() + " Max speed: " + _mover.GetMaxSpeed() + " Acceleration: " + _mover.GetAcceleration());
    }

    public void Win()
    {
        Won?.Invoke();
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

    public void StrengthenWheels()
    {
        _mover.StrengthenWheels();
    }

    public void SetCriticalRespawnOffset(float criticalOffset)
    {
        _respawner.SetCriticalHorizontalOffset(criticalOffset);
    }

    public void ResetToDefaultWheel()
    {
        _mover.ResetToDefaultWheel();
    }

    public void StopMashine()
    {
        _mover.StopMoving();
        _respawner.ProhibitRespawn();
    }

    public void StartMashine()
    {
        _mover.StartMoving();
        _respawner.AllowRespawn();
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

    public float GetCurrentSpeed()
    {
        return _mover.GetCurrentSpeed();
    }
}
public enum CarType
{
    Player,
    AI
}
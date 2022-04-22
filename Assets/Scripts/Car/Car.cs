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
    [SerializeField] private TMP_Text _nameText;

    [Header("Debug")]
    [SerializeField] private bool _showSpeed = false;

    private Mover _mover;
    private Respawner _respawner;

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

    public void SetSpeedLimit(float speed)
    {
        _mover.SetMaxSpeed(speed);
    }

    public void SetMaxOffset(float value)
    {
        _mover.SetCriticalHorizontalOffset(value);
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
        OnFinished?.Invoke(this);
    }

    public void SetName(string name)
    {
        _name = name;
        _nameText.text = name;
    }
}
public enum CarType
{
    Player,
    AI
}
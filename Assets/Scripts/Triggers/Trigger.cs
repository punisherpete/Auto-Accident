using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent ActivateAfterPlayerEnter;
    public UnityEvent ActivateAfterAIEnter;
    public UnityEvent ActivateAfterAllEnter;

    [SerializeField] private bool _isActivatedByProjector = false;

    private Car _car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            _car = car;
            ActivateEvents();
        }
        else if (other.TryGetComponent(out Projector projector))
        {
            _car = projector.GetCat();
            ActivateEvents();
        }
    }

    private void ActivateEvents()
    {
        if (_car.Type == CarType.Player)
            ActivateAfterPlayerEnter?.Invoke();
        if (_car.Type == CarType.AI)
            ActivateAfterAIEnter?.Invoke();
        ActivateAfterAllEnter?.Invoke();
    }

    public void SetSpeedLimit(float value)
    {
        _car.SetSpeedLimit(value);
    }

    public void ChangeOffsetSpeed(float speed)
    {
        _car.ChangeOffsetSpeed(speed);
    }

    public void SetMaxOffset(float horizontalOffcet)
    {
        _car.SetMaxOffset(horizontalOffcet);
    }

    public void SetCriticalRespawnOffset(float criticalOffset)
    {
        _car.SetCriticalRespawnOffset(criticalOffset);
    }

    public void SetRespawnPoint(Transform point)
    {
        _car.SetRespawnPoint(point);
    }

    public void Finish()
    {
        _car.Finish();
    }

    public void StopCar()
    {
        _car.StopMashine();
    }
}

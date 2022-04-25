using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent ActivateAfterPlayerEnter;
    public UnityEvent ActivateAfterAIEnter;
    public UnityEvent ActivateAfterAllEnter;

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

    public void Finish()
    {
        _car.Finish();
    }

    public void StopCar()
    {
        _car.StopMashine();
    }
}

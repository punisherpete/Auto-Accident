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
            if (_car.Type == CarType.Player)
                ActivateAfterPlayerEnter?.Invoke();
            if (_car.Type == CarType.AI)
                ActivateAfterAIEnter?.Invoke();
            ActivateAfterAllEnter?.Invoke();
        }
    }

    public void SetSpeedLimit(float value)
    {
        _car.SetSpeedLimit(value);
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

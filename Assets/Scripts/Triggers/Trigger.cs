using UnityEngine;
using UnityEngine.Events;
using System;

public class Trigger : MonoBehaviour
{
    public UnityEvent ActivateAfterAllEnter;
    public UnityEvent ActivateAfterPlayerEnter;
    public UnityEvent ActivateAfterAIEnter;
    public UnityEvent ActivateAfterPoliceEnter;

    [SerializeField] private bool _isActivatedByProjector = false;

    private Car _car;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivatedByProjector && other.TryGetComponent(out Projector projector))
        {
            _car = projector.GetCar();
            ActivateEvents();
        }
        else if (!_isActivatedByProjector && other.TryGetComponent(out Car car))
        {
            _car = car;
            ActivateEvents();
        }
    }

    private void ActivateEvents()
    {
        ActivateAfterAllEnter?.Invoke();

        if (_car.Type == CarType.Player)
            ActivateAfterPlayerEnter?.Invoke();
        if (_car.Type == CarType.AI)
            ActivateAfterAIEnter?.Invoke();
        if(_car.Type == CarType.Police)
            ActivateAfterPoliceEnter?.Invoke();
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
        _car.TrySetCriticalRespawnOffset(criticalOffset);
    }

    public void SetRespawnPoint(Transform point)
    {
        _car.TrySetRespawnPoint(point);
    }

    public void SetControlOnRoad(bool permission)
    {
        if (permission)
            _car.TurnControlOnRoad();
        else
            _car.TurnOffControlOnRoad();
    }

    public void DisableStrongAI()
    {
        if(_car.Type == CarType.AI)
        {
           if (_car.TryGetComponent(out AI ai))
                ai.BecomeWeak();
        }
    }

    public void TrySetNewTargetOffset(float offset)
    {
        _car.TrySetNewTargetOffset(offset);
    }
    
    public void SetNewTargetOffset(float offset)
    {
        _car.SetNewTargetOffset(offset);
    }

    public void Finish()
    {
        _car.Finish();
    }

    public void StopCar()
    {
        _car.StopMachine();
    }

    public void DisableCar()
    {
        _car.gameObject.SetActive(false);
    }
}

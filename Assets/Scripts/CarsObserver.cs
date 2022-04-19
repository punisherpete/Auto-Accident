using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarsObserver : MonoBehaviour
{
    public UnityEvent ActivateAfterPlayerWin;
    public UnityEvent ActivateAfterPlayerLose;
    public UnityEvent ActivateAfterAWhileAfterPlayerFinished;

    [SerializeField] private Transform _finishLine;
    [SerializeField] private float _activationTime;
    [SerializeField] private LeaderboardChecker _leaderboardChecker;

    private List<Car> _cars;
    private bool _isWinnerHamsterFound;
    private Transform _playerCar = null;

    private void Awake()
    {
        _cars = new List<Car>(GetComponentsInChildren<Car>());
        foreach (var car in _cars)
        {
            if (car.Type == CarType.Player)
                _playerCar = car.transform;
        }
    }

    private void OnEnable()
    {
        foreach (var car in _cars)
        {
            car.OnFinished += CheckFinished;
        }
    }

    private void OnDisable()
    {
        foreach (var car in _cars)
        {
            car.OnFinished -= CheckFinished;
        }
    }

    private void CheckFinished(Car car)
    {
        _leaderboardChecker.SetPlace(car);
        if (!_isWinnerHamsterFound)
        {
            car.Win();
            _isWinnerHamsterFound = true;
            RemoveCurrentCarFromTheList(car);
            if (car.Type == CarType.Player)
            {
                StartCoroutine(WaitAfterAWhileAfterPlayerFinishing());
                ActivateAfterPlayerWin?.Invoke();
                DeterminePlacesByDistanceToFinishLine();
            }
        }
        else
        {
            car.Lose();
            RemoveCurrentCarFromTheList(car);
            if (car.Type == CarType.Player)
            {
                StartCoroutine(WaitAfterAWhileAfterPlayerFinishing());
                ActivateAfterPlayerLose?.Invoke();
                DeterminePlacesByDistanceToFinishLine();
            }
        }
    }

    public bool IsExceedsCriticalDistanceFromPlayer(Transform originCar, float criticalDistance)
    {
        if (_playerCar == null)
            return false;
        return criticalDistance < Vector3.Distance(_playerCar.position, _finishLine.position) - Vector3.Distance(originCar.position, _finishLine.position);
    }

    public bool IsCarInSafeZone(Transform originCar, float safeDistanceForSafeMode)
    {
        foreach (var car in _cars)
        {
            if (car.transform == originCar)
                continue;
            if (Vector3.Distance(originCar.position, car.transform.position) < safeDistanceForSafeMode)
                return false;
        }
        return true;
    }

    public void StopAllCars()
    {
        foreach (var car in _cars)
        {
            car.StopMashine();
        }
    }

    public void StartAllCars()
    {
        foreach (var car in _cars)
        {
            car.StartMashine();
        }
    }

    private void RemoveCurrentCarFromTheList(Car car)
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            if (car.Name == _cars[i].Name)
            {
                _cars.RemoveAt(i);
                break;
            }
        }
    }

    private void DeterminePlacesByDistanceToFinishLine()
    {
        while (_cars.Count > 0)
        {
            int indexCarMinDistance = -1;
            for (int i = 0; i < _cars.Count; i++)
            {
                if (indexCarMinDistance == -1)
                    indexCarMinDistance = i;
                else if (Vector3.Distance(_cars[i].transform.position, _finishLine.position) <
                         Vector3.Distance(_cars[indexCarMinDistance].transform.position, _finishLine.position))
                    indexCarMinDistance = i;
            }
            if (indexCarMinDistance == -1)
                break;
            else
            {
                _cars[indexCarMinDistance].StopMashine();
                _leaderboardChecker.SetPlace(_cars[indexCarMinDistance]);
                RemoveCurrentCarFromTheList(_cars[indexCarMinDistance]);
            }
        }
    }

    private IEnumerator WaitAfterAWhileAfterPlayerFinishing()
    {
        yield return new WaitForSeconds(_activationTime);
        ActivateAfterAWhileAfterPlayerFinished?.Invoke();
    }
}

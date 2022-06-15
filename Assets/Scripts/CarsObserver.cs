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
    [SerializeField] private VariableJoystick _joystick;

    private List<Car> _cars;
    private List<Car> _allCars;
    private bool _isWinnerFound;
    private Car _playerCar = null;

    public bool IsPlayerActive => _joystick.IsPointerDown;


    private void Awake()
    {
        _cars = new List<Car>(GetComponentsInChildren<Car>());
        _allCars = new List<Car>(GetComponentsInChildren<Car>());

        foreach (var car in _cars)
        {
            if (car.Type == CarType.Player)
                _playerCar = car;
        }
    }

    private void OnEnable()
    {
        foreach (var car in _cars)
            car.OnFinished += CheckFinished;
    }

    private void OnDisable()
    {
        foreach (var car in _cars)
            car.OnFinished -= CheckFinished;
    }

    public int DetermineCurrentPlace(Car determinedCar)
    {
        var percents = new List<double>();

        foreach (var car in _allCars)
            percents.Add(car.GetCurrentSplinePercent());

        int place = 1;

        while (percents.Count > 0)
        {
            int maxPercentIndex = 0;
            for (int i = 0; i < percents.Count; i++)
            {
                if (percents[i] > percents[maxPercentIndex])
                    maxPercentIndex = i;
            }
            if (percents[maxPercentIndex] != determinedCar.GetCurrentSplinePercent())
            {
                place++;
                percents.RemoveAt(maxPercentIndex);
            }
            else
                break;
        }
        return place;
    }

    public float? DistanceAheadOfPlayer(Car origin) // merge?
    {
        if (_playerCar == null)
            return null;

        if (origin.GetCurrentSplinePercent() > _playerCar.GetCurrentSplinePercent())
            return Vector3.Distance(origin.transform.position, _playerCar.transform.position);

        return null;
    }

    public float? DistanceBehindThePlayer(Car origin) // merge?
    {
        if (_playerCar == null)
            return null;

        if (origin.GetCurrentSplinePercent() < _playerCar.GetCurrentSplinePercent())
            return Vector3.Distance(origin.transform.position, _playerCar.transform.position);

        return null;
    }

    public bool IsFasterThanPlayer(Car origin, float speedDifference) =>
        origin.GetCurrentSpeed() - _playerCar.GetCurrentSpeed() > speedDifference;

    public bool IsInSafeZone(Transform origin, float safeDistanceForSafeMode)
    {
        foreach (var car in _cars)
        {
            if (car.transform == origin)
                continue;
            
            if (Vector3.Distance(origin.position, car.transform.position) < safeDistanceForSafeMode)
                return false;
        }
        return true;
    }

    public void StopAllCars()
    {
        foreach (var car in _cars)
            car.StopMachine();
    }

    public void StartAllCars()
    {
        foreach (var car in _cars)
            car.StartMachine();
    }

    private void CheckFinished(Car car)
    {
        _leaderboardChecker.SetPlace(car);
        if (!_isWinnerFound)
        {
            car.Win();
            _isWinnerFound = true;
            RemoveCurrentCarFromTheList(car);
            if (car.Type == CarType.Player)
            {
                Finish();
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
                Finish();
                ActivateAfterPlayerLose?.Invoke();
                DeterminePlacesByDistanceToFinishLine();
            }
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
                _cars[indexCarMinDistance].StopMachine();
                _leaderboardChecker.SetPlace(_cars[indexCarMinDistance]);
                RemoveCurrentCarFromTheList(_cars[indexCarMinDistance]);
            }
        }
    }

    private void Finish() => ActivateAfterAWhileAfterPlayerFinished?.Invoke();
}

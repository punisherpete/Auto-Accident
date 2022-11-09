using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using System.Linq;

public class GAEvents : MonoBehaviour
{
    private Car _car;

    private void Awake()
    {
        OnLevelStart(SceneManager.GetActiveScene().buildIndex);

        List<Car> cars = FindObjectsOfType<Car>().ToList();

        foreach(Car car in cars)
        {
            if (car.Type == CarType.Player)
                _car = car;
        }
    }

    private void OnEnable()
    {
        if (_car == null)
            return;

        _car.Won += OnLevelComplete;
        _car.Lost += LogEventLose;
    }

    private void OnDisable()
    {
        if (_car == null)
            return;

        _car.Won -= OnLevelComplete;
        _car.Lost -= LogEventLose;
    }

    private void LogEventLose()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "fail", new Dictionary<string, object>()
        {
            {"level", SceneManager.GetActiveScene().buildIndex },
            {"time_spent", (int)Time.timeSinceLevelLoad }
        });
    }

    public void OnLevelStart(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", new Dictionary<string, object>()
        {
            {"level", level }
        });

        Debug.Log("GA - OnLevelStart - " + gameObject.name);
    }

    public void OnLevelComplete()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete", new Dictionary<string, object>()
        {
            {"level", SceneManager.GetActiveScene().buildIndex },
            {"time_spent", (int)Time.timeSinceLevelLoad }
        });

        Debug.Log("GA - OnLevelComplete");
    }
}

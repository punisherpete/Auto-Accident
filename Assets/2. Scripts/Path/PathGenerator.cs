using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private GameObject _nodePrefab;
    /*[SerializeField] private List<PathObserber> _carsPathObservers;*/
    [SerializeField] private CarPathObserber _carPathObserver;
    [SerializeField] private SplineComputer _roadSpline;

    private void Awake()
    { 
        _carPathObserver.SetPath(GeneratePath());
    }

    private PathMover GeneratePath()
    {
        GameObject path = Instantiate(_pathPrefab);
        foreach (var sample in _roadSpline.samples)
        {
            Instantiate(_nodePrefab, sample.position, sample.rotation, path.transform);
        }
        if(path.TryGetComponent(out PathMover pathController))
            return pathController; 
        else
        {
            Debug.LogError("Не найден PathController");
            return null;
        }
    }
}

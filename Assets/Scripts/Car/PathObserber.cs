using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PathObserber : MonoBehaviour
{
    [Header("Car Patch")]
    [SerializeField] private SplineComputer _spline;
    [Header("Generation")]
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private GameObject _nodePrefab;

    private PathMover _pathMover = null;
    private List<Transform> _path = null;
    private Mover _mover;
    private int _minIndex = 0;

    public void Awake()
    {
        PathMover pathController = GeneratePath();
        _path = new List<Transform>(pathController.GetComponentsInChildren<Transform>());
        _path.RemoveAt(0);
        _pathMover = pathController;
        _mover = GetComponent<Mover>();
        _mover.SetPathController(_pathMover);
    }

    private PathMover GeneratePath()
    {
        GameObject path = Instantiate(_pathPrefab);
        foreach (var sample in _spline.samples)
        {
            Instantiate(_nodePrefab, sample.position, sample.rotation, path.transform);
        }
        if (path.TryGetComponent(out PathMover pathController))
            return pathController;
        else
        {
            Debug.LogError("Не найден PathController");
            return null;
        }
    }

    private void Start()
    {
        if (_pathMover != null)
            _pathMover.Initialize();
    }

    private void FixedUpdate()
    {
        int minIndex = 0;
        float minDistance = Vector3.Distance(_path[0].position, transform.position);
        for (int i = 1; i < _path.Count; i++)
        {
            if (Vector3.Distance(_path[i].position, transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(_path[i].position, transform.position);
                minIndex = i;
            }
        }
        if(minIndex+1< _path.Count && _minIndex != minIndex)
        {
            _minIndex = minIndex;
            _mover.SetTargetNode(_path[minIndex],_path[minIndex+1]);
        }
    }

    
}

using UnityEngine;

[RequireComponent(typeof(Mover))]
public class CarPathObserber : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private PathMover _pathMover = null;

    private Transform[] _path = null;
    private Mover _mover;
    private int _minIndex = 0;

    public void SetPath(PathMover pathController)
    {
        _path = pathController.GetComponentsInChildren<Transform>();
        _pathMover = pathController;
        _mover = GetComponent<Mover>();
        _mover.SetPathController(_pathMover);
    }

    private void Start()
    {
        if (_pathMover != null)
            _pathMover.Initialize();
    }

    private void FixedUpdate()
    {
        if(_pathMover == null || _mover == null)
        {
            /*Debug.LogError("Не передан путь");*/
            return;
        }
        int minIndex = 0;
        float minDistance = Vector3.Distance(_path[0].position, transform.position);
        for (int i = 1; i < _path.Length; i++)
        {
            if (Vector3.Distance(_path[i].position, transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(_path[i].position, transform.position);
                minIndex = i;
            }
        }
        if(minIndex+1< _path.Length && _minIndex != minIndex)
        {
            _minIndex = minIndex;
            _mover.SetTargetNode(_path[minIndex],_path[minIndex+1]);
        }
    }
}

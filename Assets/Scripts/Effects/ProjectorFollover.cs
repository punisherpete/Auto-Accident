using UnityEngine;

public class ProjectorFollover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 1f;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = Vector3.Lerp(_transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
}

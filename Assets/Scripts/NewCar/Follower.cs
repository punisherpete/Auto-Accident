using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject _anchor;

    private Rigidbody _partRigidbody;

    private void Start()
    {
        _partRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _partRigidbody.MovePosition(_anchor.transform.position);
        _partRigidbody.MoveRotation(_anchor.transform.rotation);
    }
}

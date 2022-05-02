using UnityEngine;

public class StopObstacle : MonoBehaviour
{
    [SerializeField] private Collider _self;
    [SerializeField] private GameObject _arrows;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.GetComponent<Car>())
            {
                Invoke(nameof(DeactivateCollider), 0.01f);
                _arrows.SetActive(false);
            }
        }
    }

    private void DeactivateCollider()
    {
        _self.enabled = false;
    }
}

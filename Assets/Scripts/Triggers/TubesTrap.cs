using UnityEngine;

public class TubesTrap : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _tubes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Car>())
        {
            Activate();
            enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.GetComponent<InteractionProcessor>())
        {
            print("e");
            Activate();
            enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void Activate()
    {
        for (int i = 0; i < _tubes.Length; i++)
        {
            _tubes[i].isKinematic = false;
        }
    }
}

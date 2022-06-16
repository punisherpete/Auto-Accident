using UnityEngine;

public class PhysicSwitch : MonoBehaviour
{
    [SerializeField] private TrainPhysicsSwitch _trainPhysicsSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mover>())
        {
            _trainPhysicsSwitch.BecomePhysics();
        }
    }
}

using UnityEngine;

public class SpeedTrigger : MonoBehaviour
{
    [SerializeField] private float _settedSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Mover mover))
        {
            mover.SetMaxSpeed(_settedSpeed);
        }
    }
}
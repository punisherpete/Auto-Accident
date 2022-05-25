using UnityEngine;

public class ForceZone : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private ImpactDirection _impactDirection;

    private Vector3 _impact;

    private void Start()
    {
        SetImpactVector();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody rigidbody))
        {
            if (rigidbody.GetComponent<Mover>())
            {
                rigidbody.AddForce(_impact * _force, ForceMode.VelocityChange);
            }
        }
    }

    private void SetImpactVector()
    {
        switch (_impactDirection)
        {
            case ImpactDirection.Up:
                _impact = Vector3.up;
                break;
            case ImpactDirection.Right:
                _impact = transform.right;
                break;
            case ImpactDirection.Left:
                _impact = -transform.right;
                break;
            case ImpactDirection.Down:
                _impact = Vector3.down;
                break;
            default:
                break;
        }
    }
}

public enum ImpactDirection
{
    Up,
    Right,
    Left,
    Down
}

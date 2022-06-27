using UnityEngine;

public class PositionChaiser : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.position = _target.position;
    }
}

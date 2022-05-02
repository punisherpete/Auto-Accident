using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(new Vector3(0f, _speed * Time.deltaTime, 0f));
    }
}

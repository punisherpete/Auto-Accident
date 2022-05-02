using UnityEngine;

public class DustOutWheels : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leftWheelEffect;
    [SerializeField] private ParticleSystem _rightWheelEffect;

    private void FixedUpdate()
    {
        bool isAboveSand = Physics.Raycast(transform.position, Vector3.down, 0.2f, 17);
        _leftWheelEffect.gameObject.SetActive(isAboveSand);
        _rightWheelEffect.gameObject.SetActive(isAboveSand);
    }
}

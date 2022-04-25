using System.Collections;
using UnityEngine;

public class AirMovemetn : MonoBehaviour
{
    [SerializeField] private float _rotationsConstrainDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            Rigidbody target = car.GetComponent<Rigidbody>();
            StartCoroutine(DisableBodyRotationsForSeconds(_rotationsConstrainDuration, target));
        }
    }

    private IEnumerator DisableBodyRotationsForSeconds(float delay, Rigidbody carRigidbody)
    {
        carRigidbody.freezeRotation = true;
        yield return new WaitForSeconds(delay);
        carRigidbody.freezeRotation = false;

    }
}

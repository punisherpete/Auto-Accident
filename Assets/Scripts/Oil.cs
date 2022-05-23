using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    [SerializeField] private float _intencity;
    [SerializeField] private float _loseControlDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rigidbody))
        {
            if (rigidbody.GetComponent<Mover>())
            {
                rigidbody.AddTorque( new Vector3(0f, _intencity, 0f), ForceMode.VelocityChange);
                StartCoroutine(LooseControl(_loseControlDuration, rigidbody));
            }
        }
    }

    private IEnumerator LooseControl(float duration, Rigidbody rigidbody)
    {
        float t = 0;
        while (t < 1)
        {
            rigidbody.AddTorque(new Vector3(0f, 100f * _intencity, 0f), ForceMode.VelocityChange);
            t += Time.deltaTime / duration;
            yield return null;
        }
    }
}

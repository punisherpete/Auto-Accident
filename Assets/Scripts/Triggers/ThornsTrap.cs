using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThornsTrap : MonoBehaviour
{
    public event UnityAction Activated;
    public UnityEvent ActivateAfterTriggerEnter;

    [SerializeField] private float _slidingTime = 3f;
    [SerializeField] private bool _isDeactivatedAfterTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Car car))
        {
            car.SetSlidingWheel(_slidingTime);
            Activated?.Invoke();
            ActivateAfterTriggerEnter?.Invoke();
            if (_isDeactivatedAfterTriggerEnter)
                gameObject.SetActive(false);
        }
    }
}

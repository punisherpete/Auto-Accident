using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneInteraction : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>())
        {
            _animator.enabled = false;
        }
    }
}

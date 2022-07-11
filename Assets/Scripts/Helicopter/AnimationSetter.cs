using System;
using UnityEngine;

public class AnimationSetter : MonoBehaviour
{
    [SerializeField] private int _number;

    public event Action<int> Reached;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInput>())
        {
            Reached?.Invoke(_number);
            gameObject.SetActive(false);
        }
    }
}

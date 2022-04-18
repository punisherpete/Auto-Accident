using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _smashTrigger;
    [SerializeField] private string _fadeTrigger;
    [SerializeField] private float _fadeDelay = 3f;

    private int _smashHash;
    private int _fadeHash; 

    private void Start()
    {
        _smashHash = Animator.StringToHash(_smashTrigger);
        _fadeHash = Animator.StringToHash(_fadeTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageEffector>())
        {
            _animator.Play(_smashHash);
            Invoke(nameof(OnFadeAnimation), _fadeDelay);
        }
    }

    private void OnFadeAnimation()
    {
        _animator.Play(_fadeHash);
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}

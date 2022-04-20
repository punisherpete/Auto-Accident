using UnityEngine;

public class JointAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _hitAnimations;

    private int _plays;
    private int _firstHitHash;
    private string _firstHit = "FirstHit";


    private void Start()
    {
        _firstHitHash = Animator.StringToHash(_firstHit);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.2f)
        {
            if (_plays <= 0)
            {
                _animator.SetTrigger(_firstHitHash);
                _plays += 1;
            }
            else
            {
                string random = "" + Random.Range(1, _hitAnimations + 1);
                int randomHash = Animator.StringToHash(random);
                _animator.SetTrigger(randomHash);
            }
        }
    }
}

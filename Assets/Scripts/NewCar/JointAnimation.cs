using System.Collections;
using UnityEngine;

public class JointAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _hitAnimations;
    [SerializeField] private InteractionProcessor _affectedPart;
    [SerializeField] private float _toreOffDelay = 5f;

    private int _plays;
    private int _firstHitHash;
    private string _firstHit = "FirstHit";


    private void OnEnable()
    {
        _affectedPart.Affected += OnPlayAnimation;
    }

    private void Start()
    {
        _firstHitHash = Animator.StringToHash(_firstHit);
    }

    private void OnDisable()
    {
        _affectedPart.Affected -= OnPlayAnimation;
    }

    private void OnPlayAnimation(InteractionProcessor interaction)
    {
        if (_plays <= 0)
        {
            _animator.SetTrigger(_firstHitHash);
            _plays += 1;
            StartCoroutine(ToreOff(_toreOffDelay));
        }
        else
        {
            string random = "" + Random.Range(1, _hitAnimations + 1);
            int randomHash = Animator.StringToHash(random);
            _animator.SetTrigger(randomHash);
        }
    }

    private IEnumerator ToreOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}

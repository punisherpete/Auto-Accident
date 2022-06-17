using UnityEngine;

public class HelicopterAnimation : MonoBehaviour
{
    [SerializeField] private AnimationSetter[] _frontiers;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        for (int i = 0; i < _frontiers.Length; i++)
        {
            _frontiers[i].Reached += OnSetAnimatorTrigger;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _frontiers.Length; i++)
        {
            _frontiers[i].Reached -= OnSetAnimatorTrigger;
        }
    }

    private void OnSetAnimatorTrigger(int id)
    {
        _animator.SetInteger("Fly", id);
    }

}

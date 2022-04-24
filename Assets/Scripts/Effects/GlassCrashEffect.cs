using UnityEngine;

public class GlassCrashEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dropsEffect;
    [SerializeField] private GameObject _completeGlass;
    [SerializeField] private InteractionProcessor[] _makeAffectionParts;

    private void OnEnable()
    {
        for (int i = 0; i < _makeAffectionParts.Length; i++)
        {
            _makeAffectionParts[i].Affected += OnPlayEffect;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _makeAffectionParts.Length; i++)
        {
            _makeAffectionParts[i].Affected -= OnPlayEffect;
        }
    }

    private void OnPlayEffect(InteractionProcessor interactionProcessor)
    {
        _completeGlass.SetActive(false);
        _dropsEffect.Play(true);
        enabled = false;
    }
}

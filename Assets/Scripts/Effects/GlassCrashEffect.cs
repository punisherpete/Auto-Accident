using System.Collections.Generic;
using UnityEngine;

public class GlassCrashEffect : MonoBehaviour
{
    [SerializeField] private List<GlassPart> _partsOfGlass;
    [SerializeField] private InteractionProcessor[] _makeAffectionParts;
    [SerializeField] private GameObject[] _completeGlass;
    [SerializeField] private float _crashForce;

    private void Awake()
    {
        for (int i = 0; i < _makeAffectionParts.Length; i++)
        {
            _makeAffectionParts[i].Affected += OnBurstEffect;
        }

        for (int i = 0; i < _partsOfGlass.Count; i++)
        {
            _partsOfGlass[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _makeAffectionParts.Length; i++)
        {
            _makeAffectionParts[i].Affected -= OnBurstEffect;
        }
    }

    private void OnBurstEffect(InteractionProcessor processor, Vector3 postition)
    {
        for (int i = 0; i < _completeGlass.Length; i++)
        {
            _completeGlass[i].SetActive(false);
        }
        for (int i = 0; i < _partsOfGlass.Count; i++)
        {
            _partsOfGlass[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < _partsOfGlass.Count; i++)
        {
            if (_partsOfGlass[i])
            {
                _partsOfGlass[i].Crash(_crashForce);
                Destroy(_partsOfGlass[i].gameObject, 5f);
                _partsOfGlass.Remove(_partsOfGlass[i]);
            }
        }
    }
}

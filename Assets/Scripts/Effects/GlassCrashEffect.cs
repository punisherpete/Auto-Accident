using System.Collections.Generic;
using UnityEngine;

public class GlassCrashEffect : MonoBehaviour
{
    [SerializeField] private List<GlassPart> _partsOfGlass;
    [SerializeField] private InteractionProcessor[] _makeAffectionParts;
    [SerializeField] private GameObject[] _completeGlass;
    [SerializeField] private float _crashForce;

    private int _crashCount;

    private void OnEnable()
    {
        for (int i = 0; i < _makeAffectionParts.Length; i++)
        {
            _makeAffectionParts[i].Affected += OnEffect;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _partsOfGlass.Count; i++)
        {
            _partsOfGlass[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _makeAffectionParts.Length; i++)
        {
            _makeAffectionParts[i].Affected -= OnEffect;
        }
    }

    private void OnEffect(InteractionProcessor processor)
    {
        DisableCompleteGlass();

        if (_crashCount >= Random.Range(10f, 50f))
        {
            Burst();
        }
        else
        {
            MakeCracks();
        }
    }

    private void DisableCompleteGlass()
    {
        for (int i = 0; i < _completeGlass.Length; i++)
        {
            _completeGlass[i].SetActive(false);
        }
    }

    private void MakeCracks()
    {
        for (int i = 0; i < _partsOfGlass.Count; i++)
        {
            _partsOfGlass[i].gameObject.SetActive(true);
        }
        _crashCount += 1;
    }

    private void Burst()
    {
        for (int i = 0; i < _partsOfGlass.Count; i++)
        {
            _partsOfGlass[i].Crash(_crashForce);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _deformableViews;
    [SerializeField] private NewInteractionProcessor[] _firstMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _secondMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _thirdMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _fourthMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _fifthMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _sixthMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _seventhMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _eighthMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _neinhthMeshChangingParts;
    [SerializeField] private NewInteractionProcessor[] _tenhthMeshChangingParts;

    [SerializeField] private float _deformingForce = 5f;
    [SerializeField, Range(0f, 1f)] private float _deformationSensitivity = 1f;

    [SerializeField] private EffectsGenerator _effectsGemerator;

    private List<NewInteractionProcessor[]> _newInteractionProcessors = new List<NewInteractionProcessor[]>();
    private Dictionary<NewInteractionProcessor, int> _blendShapesNumberMatch = new Dictionary<NewInteractionProcessor, int>();
    private Dictionary<NewInteractionProcessor, SkinnedMeshRenderer> _blendShapesMatch = new Dictionary<NewInteractionProcessor, SkinnedMeshRenderer>();

    private void Awake()
    {
        MakeProcessorsArchive();

        for (int i = 0; i < _deformableViews.Length; i++)
        {
            ConductMatch(_newInteractionProcessors[i], _deformableViews[i]);
        }
    }

    private void MakeProcessorsArchive()
    {
        _newInteractionProcessors.Add(_firstMeshChangingParts);
        _newInteractionProcessors.Add(_secondMeshChangingParts);
        _newInteractionProcessors.Add(_thirdMeshChangingParts);
        _newInteractionProcessors.Add(_fourthMeshChangingParts);
        _newInteractionProcessors.Add(_fifthMeshChangingParts);
        _newInteractionProcessors.Add(_sixthMeshChangingParts);
        _newInteractionProcessors.Add(_seventhMeshChangingParts);
        _newInteractionProcessors.Add(_eighthMeshChangingParts);
        _newInteractionProcessors.Add(_neinhthMeshChangingParts);
        _newInteractionProcessors.Add(_tenhthMeshChangingParts);
    }

    private void ConductMatch(NewInteractionProcessor[] responsibleParts, SkinnedMeshRenderer deformableMesh)
    {
        for (int i = 0; i < responsibleParts.Length; i++)
        {
            _blendShapesNumberMatch.Add(responsibleParts[i], i);
            _blendShapesMatch.Add(responsibleParts[i], deformableMesh);
        }
    }

    private void OnEnable()
    {
        SubscribeForDamage(_firstMeshChangingParts);
        SubscribeForDamage(_secondMeshChangingParts);
        SubscribeForDamage(_thirdMeshChangingParts);
        SubscribeForDamage(_fourthMeshChangingParts);
        SubscribeForDamage(_fifthMeshChangingParts);
        SubscribeForDamage(_sixthMeshChangingParts);
        SubscribeForDamage(_seventhMeshChangingParts);
        SubscribeForDamage(_eighthMeshChangingParts);
        SubscribeForDamage(_neinhthMeshChangingParts);
        SubscribeForDamage(_tenhthMeshChangingParts);
    }

    private void OnDisable()
    {
        UnSubscribeForDamage(_firstMeshChangingParts);
        UnSubscribeForDamage(_secondMeshChangingParts);
        UnSubscribeForDamage(_thirdMeshChangingParts);
        UnSubscribeForDamage(_fourthMeshChangingParts);
        UnSubscribeForDamage(_fifthMeshChangingParts);
        UnSubscribeForDamage(_sixthMeshChangingParts);
        UnSubscribeForDamage(_seventhMeshChangingParts);
        UnSubscribeForDamage(_eighthMeshChangingParts);
        UnSubscribeForDamage(_neinhthMeshChangingParts);
        UnSubscribeForDamage(_tenhthMeshChangingParts);
    }

    private void SubscribeForDamage(NewInteractionProcessor[] meshChangingParts)
    {
        if (meshChangingParts.Length == 0)
            return;

        for (int i = 0; i < meshChangingParts.Length; i++)
        {
            meshChangingParts[i].Affected += OnTakeDamage;
        }
    }

    private void UnSubscribeForDamage(NewInteractionProcessor[] meshChangingParts)
    {
        if (meshChangingParts.Length == 0)
            return;

        for (int i = 0; i < meshChangingParts.Length; i++)
        {
            meshChangingParts[i].Affected -= OnTakeDamage;
        }
    }

    private void OnTakeDamage(NewInteractionProcessor affectedPart, Vector3 position)
    {
        SkinnedMeshRenderer targetMeshRenderer = null;
        foreach (var blendShapesMatch in _blendShapesMatch)
        {
            if (affectedPart == blendShapesMatch.Key)
                targetMeshRenderer = blendShapesMatch.Value;
        }

        if (targetMeshRenderer)
        {

            foreach(var numberMatch in _blendShapesNumberMatch)
            {
                if(affectedPart == numberMatch.Key)
                {
                    float deformingPartCurrentHealth = targetMeshRenderer.GetBlendShapeWeight(numberMatch.Value);
                    targetMeshRenderer.SetBlendShapeWeight(numberMatch.Value, deformingPartCurrentHealth + _deformingForce * _deformationSensitivity);
                    _effectsGemerator.Play(position);
                }
            }
        }

    }
}

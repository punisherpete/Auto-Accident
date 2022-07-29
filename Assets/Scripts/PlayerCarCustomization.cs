using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarCustomization : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _mainCar;
    [SerializeField] private List<SkinnedMeshRenderer> _otherCarParts;

    public void ApplyCustomization(Material mainMat, Material otherMat)
    {
        _mainCar.material = mainMat;

        foreach (var renderer in _otherCarParts)
            renderer.material = otherMat;
    }
}

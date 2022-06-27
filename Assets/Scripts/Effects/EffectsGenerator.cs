using System.Collections.Generic;
using UnityEngine;

public class EffectsGenerator : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _effects;

    private void Awake()
    {
        Play(-transform.up * 100);
    }

    public void Play(Vector3 position)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            _effects[i].transform.position = position;
            _effects[i].Play();
        }
    }
}

using UnityEngine;

public class EffectsGenerator : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _effects;

    public void Play(Vector3 spawnPosition)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            ParticleSystem generatedEffect = Instantiate(_effects[i], spawnPosition, Quaternion.identity, transform);
            Destroy(generatedEffect.gameObject, generatedEffect.main.duration + generatedEffect.main.duration);
        }
    }
}

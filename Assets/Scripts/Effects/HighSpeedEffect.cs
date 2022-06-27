using UnityEngine;

public class HighSpeedEffect : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private ParticleSystem[] _windEffects;
    [Header("Options")]
    [SerializeField] private Mover _carMover;
    [SerializeField] private float _emittingSpeed = 20f;

    private void Update()
    {
        if(_carMover.GetCurrentSpeed() > _emittingSpeed)
        {
            for (int i = 0; i < _windEffects.Length; i++)
            {
                _windEffects[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < _windEffects.Length; i++)
            {
                _windEffects[i].gameObject.SetActive(false);
            }
        }
    }
}

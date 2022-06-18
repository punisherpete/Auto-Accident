using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingFeedBack : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _feedbackEffects;
    [SerializeField] private WheelController _wheelsController;
    [SerializeField] private PathController _pathController;
    [SerializeField] private HeightChaser _heightChaser;

    private void OnEnable()
    {
        _heightChaser.SearchingHeightReached += OnStartChasingPerfectLand;
    }

    private void OnDisable()
    {
        _heightChaser.SearchingHeightReached -= OnStartChasingPerfectLand;
    }

    private void OnStartChasingPerfectLand()
    {
        _heightChaser.enabled = false;
        StartCoroutine(ChasingPerfectLand());
    }

    private IEnumerator ChasingPerfectLand()
    {
        while (true)
        {
            if (_wheelsController.IsGrounded)
            {
                for (int i = 0; i < _feedbackEffects.Length; i++)
                {
                    _feedbackEffects[i].Play();
                }
                _heightChaser.enabled = true;
                yield break;
            }
            yield return null;
        }

    }
}

using Cinemachine;
using System.Collections;
using UnityEngine;

public class TubesTrap : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _tubes;
    [SerializeField] private GameObject[] _leanOnSticks;
    [SerializeField] private ParticleSystem _stickBurst;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Car>())
        {
            DeactivateSticks();
            ActivateTubes();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void ActivateTubes()
    {
        for (int i = 0; i < _tubes.Length; i++)
        {
            _tubes[i].isKinematic = false;
        }
    }

    private void DeactivateSticks()
    {
        for (int i = 0; i < _leanOnSticks.Length; i++)
        {
            _leanOnSticks[i].SetActive(false);
            ParticleSystem newEffect = Instantiate(_stickBurst, _leanOnSticks[i].transform.position, Quaternion.identity, null);
            newEffect.transform.localScale = _leanOnSticks[i].transform.localScale;
            newEffect.Play();
        }
    }
}

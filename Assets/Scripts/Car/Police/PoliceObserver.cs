using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceObserver : MonoBehaviour
{
    [SerializeField] private float _disableDelay = 3f;
    private PoliceAI[] _police;

    private void OnEnable()
    {
        _police = GetComponentsInChildren<PoliceAI>();
    }

    public void ActivateAllPolice()
    {
        EnablePolice();
    }

    public void DeactivateAllPolise()
    {
        foreach (var ai in _police)
            ai.BecomeWeak();

        Invoke(nameof(DisablePolice), _disableDelay);
    }

    private void EnablePolice()
    {
        foreach (var ai in _police)
            ai.StartChase();
    }

    private void DisablePolice()
    {
        foreach (var ai in _police)
            ai.StopChase();
    }
}

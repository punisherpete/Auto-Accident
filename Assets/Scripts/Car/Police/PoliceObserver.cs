using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceObserver : MonoBehaviour
{
    [SerializeField] private PoliceAI[] _police;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        _police = GetComponentsInChildren<PoliceAI>();
    }

    public void CallIn()
    {
        Invoke(nameof(EnablePolice), _delay);
    }

    public void CallOut()
    {
        foreach (var ai in _police)
            ai.BecomeWeak();

        Invoke(nameof(DisablePolice), _delay);
    }

    private void EnablePolice()
    {
        foreach (var ai in _police)
            ai.Start—hase();
    }

    private void DisablePolice()
    {
        foreach (var ai in _police)
            ai.StopChase();
    }
}

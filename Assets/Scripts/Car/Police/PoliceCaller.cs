using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCaller : MonoBehaviour
{
    [SerializeField] private AI[] _police;
    [SerializeField] private float _delay;

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
        foreach (var car in _police)
            car.gameObject.SetActive(true);
    }

    private void DisablePolice()
    {
        foreach (var car in _police)
            car.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PoliceAI : AI
{
    [SerializeField] private float _delay;

    public void Start—hase()
    {
        InvokeRepeating(nameof(ChangeHorizontalOffset), _delay, _delay);
        _car.StartMachine();
    }

    public void StopChase()
    {
        _car.StopMachine();
    }

    private void ChangeHorizontalOffset()
    {
        float offset = Random.Range(-_mover.GetCriticalOffset(), _mover.GetCriticalOffset());
        _mover.TrySetNewTargetOffset(offset);
    }
}

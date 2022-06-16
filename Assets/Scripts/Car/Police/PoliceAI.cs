using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PoliceAI : AI
{
    [SerializeField] private float _delay;

    private Mover _mover;

    private void Start()
    {
        _mover = GetComponent<Mover>();
        InvokeRepeating(nameof(ChangeHorizontalOffset), _delay, _delay);
    }

    private void ChangeHorizontalOffset()
    {
        float offset = Random.Range(-10f, 10f);
        _mover.TryChangeHorizontalOffset(offset);
    }
}

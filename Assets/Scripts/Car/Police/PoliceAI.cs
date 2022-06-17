using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PoliceAI : AI
{
    [SerializeField] private float _offsetDelay;
    [SerializeField] private bool _changeOffsetPermission;

    private bool _deactivatePermission = false;

    private void Update()
    {
        if(_observer.DistanceBehindThePlayer(_car)>150 && _deactivatePermission)
            gameObject.SetActive(false);
    }

    public void Start—hase()
    {
        if(_changeOffsetPermission)
            InvokeRepeating(nameof(ChangeHorizontalOffset), _offsetDelay, _offsetDelay);
        _deactivatePermission = true;
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

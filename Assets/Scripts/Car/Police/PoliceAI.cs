using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PoliceAI : AI
{
    [SerializeField] private float _offsetDelay;
    [SerializeField] private bool _changeOffsetPermission;

    private bool _deactivatePermission = false;

    private void FixedUpdate()
    {
        if(_observer.DistanceBehindThePlayer(_car)>150 && _deactivatePermission)
            gameObject.SetActive(false);
        _speedLimit.SetRegularDragForce(CalculateDragForce());
        DetermineSpeed();
    }

    public void StartChase()
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

    private new float CalculateDragForce()
    {
        float? distance = _observer.DistanceAheadOfPlayer(_car);

        if (distance == null)
            return 0;
        if (distance > _cheaterLeadDistance)
            return _impossibleDragModifier;
        else if (distance > _strongLeadDistance)
            return _strongDragModifier;
        else if (distance > _criticalLeadDistance)
            return _observer.IsFasterThanPlayer(_car, _criticalSpeedDifference)
                ? _strongDragModifier : _dragModifier;
        return 0;
    }

    private void ChangeHorizontalOffset()
    {
        float offset = Random.Range(-_mover.GetCriticalOffset(), _mover.GetCriticalOffset());
        _mover.TrySetNewTargetOffset(offset);
    }
}

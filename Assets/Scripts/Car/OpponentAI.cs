using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : AI
{
    private void FixedUpdate()
    {
        _speedLimit.SetRegularDragForce(CalculateDragForce());
        DetermineSpeed();
    }
}

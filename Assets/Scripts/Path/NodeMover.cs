using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour
{
    private Vector3 _startXPosition;
    private Vector3 _lastPosition;

    private void OnEnable()
    {
        _startXPosition = transform.position;
    }

    public void SetOffset(float criticalOffset, float offsetSpeed,float input)
    {
        if (Vector3.Distance(transform.position, _startXPosition) < criticalOffset && Vector3.Distance(transform.position, _startXPosition) > -criticalOffset)
        {
            _lastPosition = transform.position;
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        }
        else
            transform.position = _lastPosition;
    }
}

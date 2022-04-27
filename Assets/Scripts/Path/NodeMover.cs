using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _lastPosition;

    private void OnEnable()
    {
        _startPosition = transform.position;
    }

    public void Move(float criticalOffset, float offsetSpeed,float input)
    {
        
        if (Vector3.Distance(transform.position, _startPosition) < criticalOffset)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if(input < 0 && transform.InverseTransformPoint(_startPosition).x < 0)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if(input > 0 && transform.InverseTransformPoint(_startPosition).x > 0)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if(Vector3.Distance(transform.position, _startPosition) > criticalOffset + 0.5f)
        {
            if (transform.InverseTransformPoint(_startPosition).x < 0)
                transform.Translate(transform.right * -offsetSpeed * Time.deltaTime, Space.World);
            else if (transform.InverseTransformPoint(_startPosition).x > 0)
                transform.Translate(transform.right * offsetSpeed * Time.deltaTime, Space.World);
        }
        
    }
}

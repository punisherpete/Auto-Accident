using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointMover : MonoBehaviour
{
    private Vector3 _startPosition;

    private void OnEnable()
    {
        _startPosition = new Vector3(0,0,0);
    }

    public void Move(float criticalOffset, float offsetSpeed, float input)
    {
        if (Vector3.Distance(transform.localPosition, _startPosition) < criticalOffset)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if (input > 0 && transform.localPosition.x < 0)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if (input < 0 && transform.localPosition.x > 0)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if (Vector3.Distance(transform.localPosition, _startPosition) > criticalOffset + 0.5f)
        {
            Debug.Log("!!!!");
            if (transform.localPosition.x < 0)
                transform.Translate(transform.right * offsetSpeed * Time.deltaTime, Space.World);
            else if (transform.localPosition.x > 0)
                transform.Translate(transform.right * -offsetSpeed * Time.deltaTime, Space.World);
        }
    }
}
